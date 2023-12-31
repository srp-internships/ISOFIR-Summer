﻿using System.Globalization;
using AutoMapper;
using OfficeOpenXml;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class RestService : IRestService
{
    private readonly IClientRepository _clientRepository;
    private readonly IFirmRepository _firmRepository;
    private readonly IInvoiceLogRepository _invoiceLogRepository;
    private readonly IMapper _mapper;
    private readonly IMoveProductLogRepository _moveProductLogRepository;
    private readonly IProductRepository _productRepository;
    private readonly IRestProductRepository _restProductRepository;
    private readonly ISaleLogRepository _saleLogRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IRateService _rateService;

    public RestService(IFirmRepository firmRepository, IClientRepository clientRepository,
        IRestProductRepository restProductRepository, IStorageRepository storageRepository,
        IInvoiceLogRepository invoiceLogRepository, ISaleLogRepository saleLogRepository, IMapper mapper,
        IProductRepository productRepository, IMoveProductLogRepository moveProductLogRepository, IRateService rateService)
    {
        _firmRepository = firmRepository;
        _clientRepository = clientRepository;
        _restProductRepository = restProductRepository;
        _storageRepository = storageRepository;
        _invoiceLogRepository = invoiceLogRepository;
        _saleLogRepository = saleLogRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _moveProductLogRepository = moveProductLogRepository;
        _rateService = rateService;
    }

    public async Task<Result> InvoiceAsync(InvoiceRequestModel log)
    {
        try
        {
            var firm = await _firmRepository.GetByIdAsync(log.FirmId);
            if (firm is null)
                return new ErrorResult(new Exception(), "Фирма не действительна");


            if (!await _storageRepository.ExistByIdAsync(log.StorageId))
                return new ErrorResult(new Exception(), "Склад не действителен");


            var rest = await _restProductRepository.GetRestForInvoiceAsync(log.ProductId, log.StorageId, log.PriceUsd,
                log.PriceTjs, log.UserId);

            rest.Quantity += log.Quantity;

            await _restProductRepository.SaveChangesAsync();

            var invoiceLog = _mapper.Map<InvoiceRequestModel, InvoiceLog>(log);
            invoiceLog.RestProductId = rest.Id;
            await _invoiceLogRepository.AddAsync(invoiceLog);

            firm.CashUsd += invoiceLog.PriceUsd;

            await _firmRepository.SaveChangesAsync();
            await _invoiceLogRepository.SaveChangesAsync();

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> SaleAsync(SaleRequestModel log)
    {
        try
        {
            var rest = await _restProductRepository.GetByIdAsync(log.RestProductId);
            if (rest == null)
                return new ErrorResult(new Exception(), "Источник продукта не действителен");

            var client = await _clientRepository.GetByIdAsync(log.ClientId);
            if (client == null)
                return new ErrorResult(new Exception(), "Клиент не действительный");

            rest.Quantity -= log.Quantity;
            client.Income += (log._priceTjs - rest.InvoicePriceTjs) * log.Quantity;
            client.CashTjs -= log._priceTjs * log.Quantity;
            client.Sales += log._priceUsd * log.Quantity;

            await _restProductRepository.SaveChangesAsync();
            await _clientRepository.SaveChangesAsync();

            var saleLog = _mapper.Map<SaleRequestModel, SaleLog>(log);
            await _saleLogRepository.AddAsync(saleLog);
            await _saleLogRepository.SaveChangesAsync();

            await _restProductRepository.SaveChangesAsync();
            await _clientRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }

        return new OkResult();
    }

    public async Task<Result> GetListOfRestProductsByCategoryAsync(int categoryId)
    {
        try
        {
            var products =
                await _restProductRepository.GetRestByCategory(categoryId);

            return new OkResult<List<GetRestProductsNameResponseModel>>(products
                .Select(s => _mapper.Map<RestProduct, GetRestProductsNameResponseModel>(s)).DistinctBy(s => s.Name)
                .ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetRestsByProductAsync(int productId, int userId)
    {
        try
        {
            var products =
                await _restProductRepository.GetRestByProductIdAsync(productId, userId);
            return new OkResult<List<GetRestsByProductResponseModel>>(
                products.Select(s => _mapper.Map<RestProduct, GetRestsByProductResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetRestByFilterAsync(RestFilterRequestModel model)
    {
        try
        {
            var products =
                await _restProductRepository.GetByStorageFirmProductIds(model.ProductId, model.StorageId, model.FirmId,
                    model.UserId);

            return new OkResult<List<RestProductResponseModel>>(
                products.Select(s => _mapper.Map<RestProduct, RestProductResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> MoveProductsAsync(List<MoveRequestModel> moveRequestModel)
    {
        try
        {
            foreach (var model in moveRequestModel)
            {
                var res = await MoveProductAsync(model);
                if (res is ErrorResult) return res;
            }

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> InvoiceFromFileAsync(string path, bool isReplaceMode,int userId)
    {
        try
        {
            var rate = ((OkResult<decimal>)await _rateService.GetLastAsync(userId)).Result;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pack = new ExcelPackage(path);
            var sheet = pack.Workbook.Worksheets.First();
            var i = 2;
            while (sheet.Cells["B" + i].Value + "" != "")
            {
                var log = new InvoiceRequestModel();
                var date = sheet.Cells["G" + i].Value + "";
                if (!DateTime.TryParse(date,new CultureInfo("ru-RU"),DateTimeStyles.None, out var restDate))
                    return new ErrorResult(new Exception(), $"Недопустимая дата строка = {i}");
                
                if (!int.TryParse(sheet.Cells["C" + i].Value.ToString(),out var restQuantity))
                    return new ErrorResult($"Недопустимое количество строка = {i}");

                if (!decimal.TryParse(sheet.Cells["D" + i].Value + "", out var restPrice))
                    return new ErrorResult(new Exception(), $"Недопустимая цена строка = {i}");

                var firmId = await _firmRepository.GetIdByNameAsync(sheet.Cells["F" + i].Value + "");
                if (firmId == null)
                    return new ErrorResult(new Exception(), $"Фирма не действительна строка = {i}");

                var storageId = await _storageRepository.GetIdByNameAsync(sheet.Cells["E" + i].Value + "");
                if (storageId == null)
                    return new ErrorResult(new Exception(), $"Склад не действителен строка = {i}");

                var productId = await _productRepository.GetIdByNameAsync(sheet.Cells["B" + i].Value + "");
                if (productId == null)
                    return new ErrorResult(new Exception(), $"Продукт не действительна строка = {i}");

                log.UserId = userId;
                log.Quantity = restQuantity;
                log.DateTime = restDate;
                log.PriceUsd = restPrice;
                log.PriceTjs = restPrice * rate;
                log.FirmId = (int)firmId;
                log.StorageId = (int)storageId;
                log.ProductId = (int)productId;

                var rest = await _restProductRepository.GetRestForInvoiceAsync(log.ProductId, log.StorageId, log.PriceUsd,
                    log.PriceTjs, log.UserId);

                rest.Quantity += log.Quantity;

                await _restProductRepository.SaveChangesAsync();

                var invoiceLog = _mapper.Map<InvoiceRequestModel, InvoiceLog>(log);
                invoiceLog.RestProductId = rest.Id;
                invoiceLog.UserId = userId;
                await _invoiceLogRepository.AddAsync(invoiceLog);

                i++;
            }

            await _restProductRepository.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    private async Task<Result> MoveProductAsync(MoveRequestModel model)
    {
        if (model.Quantity <= 0)
            return new OkResult();

        var from = await _restProductRepository.GetByIdAsync(model.FromRestId);
        if (from == null)
            return new ErrorResult(new Exception(), "Не удаётся найти склад с которого вы хотите переместить продукты");


        var storage = await _storageRepository.GetByIdAsync(model.ToStorageId);
        if (storage == null)
            return new ErrorResult(new Exception(), "Не удаётся найти склад в которого вы хотите переместить продукты");

        var to = await _storageRepository.GetRestByProductIdAsync(from, model.ToStorageId);

        if (from.Quantity < model.Quantity)
            return new ErrorResult(new Exception(), "Невозможно переместить такое количество продуктов");

        from.Quantity -= model.Quantity;

        to.Quantity += model.Quantity;

        var moveProductLog = new MoveProductLog
        {
            FromStorageId = from.StorageId,
            ToStorageId = model.ToStorageId,
            ProductId = from.ProductId,
            Quantity = model.Quantity
        };

        await _moveProductLogRepository.AddAsync(moveProductLog);

        await _moveProductLogRepository.SaveChangesAsync();
        await _storageRepository.SaveChangesAsync();
        await _restProductRepository.SaveChangesAsync();
        return new OkResult();
    }
}
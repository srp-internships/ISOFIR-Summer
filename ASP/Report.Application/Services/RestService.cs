using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class RestService : IRestService
{
    private readonly IFirmRepository _firmRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IRestProductRepository _restProductRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IInvoiceLogRepository _invoiceLogRepository;
    private readonly ISaleLogRepository _saleLogRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMoveProductLogRepository _moveProductLogRepository;
    private readonly IMapper _mapper;

    public RestService(IFirmRepository firmRepository, IClientRepository clientRepository,
        IRestProductRepository restProductRepository, IStorageRepository storageRepository,
        IInvoiceLogRepository invoiceLogRepository, ISaleLogRepository saleLogRepository, IMapper mapper, IProductRepository productRepository, IMoveProductLogRepository moveProductLogRepository)
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
    }

    public async Task<Result> InvoiceAsync(InvoiceRequestModel log)
    {
        try
        {
            if (!await _firmRepository.ExistByIdAsync(log.FirmId))
                return new ErrorResult(new Exception(), "Фирма не действительна");
            

            if (!await _storageRepository.ExistByIdAsync(log.StorageId))
                return new ErrorResult(new Exception(), "Склад не действителен");
            

            var rest = await _restProductRepository.GetByIdAsync(log.ProductId);
            if (rest == null)
            {
                rest = new RestProduct
                {
                    ProductId = log.ProductId,
                    StorageId = log.StorageId
                };
                await _restProductRepository.AddAsync(rest);
            }

            rest.Quantity += log.Quantity;

            await _restProductRepository.SaveChangesAsync();

            var invoiceLog = _mapper.Map<InvoiceRequestModel, InvoiceLog>(log);
            invoiceLog.RestProductId = rest.Id;
            await _invoiceLogRepository.AddAsync(invoiceLog);
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
            var rest = await _restProductRepository.GetByIdAsync(log.RestId);
            if (rest == null)
                return new ErrorResult(new Exception(), "Источник продукта не действителен");

            var client = await _clientRepository.GetByIdAsync(log.ClientId);
            if (client == null)
                return new ErrorResult(new Exception(), "Клиент не действительный");

            rest.Quantity -= log.Quantity;
            client.Income += (log.SalePriceTjs - rest.InvoicePriceTjs) * log.Quantity;
            client.CashTjs -= log.SalePriceTjs * log.Quantity;

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
                .Select(s => _mapper.Map<RestProduct,GetRestProductsNameResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetRestsByProductAsync(int productId)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product==null)
            {
                return new ErrorResult(new Exception(), "Источник продукта не действителен");
            }
            var products =
                await _restProductRepository.GetRestByCategory(product.CategoryId);
            return new OkResult<List<GetRestsByProductResponseModel>>(
                products.Select(s=>_mapper.Map<RestProduct,GetRestsByProductResponseModel>(s)).ToList());
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
                await _restProductRepository.GetByStorageFirmProductIds(model.ProductId, model.StorageId, model.FirmId);
            
            return new OkResult<List<RestProductResponseModel>>(
                products.Select(s=>_mapper.Map<RestProduct,RestProductResponseModel>(s)).ToList());
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
                if (res is ErrorResult)
                {
                    return res;
                }
            }

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    private async Task<Result> MoveProductAsync(MoveRequestModel model)
    {
        if (model.Quantity<=0)
            return new OkResult();
        
        var from = await _restProductRepository.GetByIdAsync(model.FromRestId);
        if (from == null)
            return new ErrorResult(new Exception(), "Не удаётся найти склад с которого вы хотите переместить продукты");


        var storage = await _storageRepository.GetByIdAsync(model.ToStorageId);
        if (storage == null)
            return new ErrorResult(new Exception(), "Не удаётся найти склад в которого вы хотите переместить продукты");
        
        var to = await _storageRepository.GetRestByProductIdAsync(from,model.ToStorageId);

        if (from.Quantity<model.Quantity)
            return new ErrorResult(new Exception(), "Невозможно переместить такое количество продуктов");

        from.Quantity -= model.Quantity;

        to.Quantity += model.Quantity;

        var moveProductLog = new MoveProductLog
        {
            FromStorageId = from.StorageId,
            ToStorageId = model.ToStorageId,
            ProductId = from.ProductId,
            Quantity = model.Quantity,
        };
        
        await _moveProductLogRepository.AddAsync(moveProductLog);

        await _moveProductLogRepository.SaveChangesAsync();
        await _storageRepository.SaveChangesAsync();
        await _restProductRepository.SaveChangesAsync();
        return new OkResult();
    }
}
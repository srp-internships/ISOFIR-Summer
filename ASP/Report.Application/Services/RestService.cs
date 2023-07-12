using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using Report.Core.Models;

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
    private readonly IMapper _mapper;

    public RestService(IFirmRepository firmRepository, IClientRepository clientRepository,
        IRestProductRepository restProductRepository, IStorageRepository storageRepository,
        IInvoiceLogRepository invoiceLogRepository, ISaleLogRepository saleLogRepository, IMapper mapper, IProductRepository productRepository)
    {
        _firmRepository = firmRepository;
        _clientRepository = clientRepository;
        _restProductRepository = restProductRepository;
        _storageRepository = storageRepository;
        _invoiceLogRepository = invoiceLogRepository;
        _saleLogRepository = saleLogRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<Result> Invoice(InvoiceRequestModel log)
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
                _restProductRepository.Add(rest);
            }

            rest.Quantity += log.Quantity;

            _restProductRepository.SaveChanges();

            var invoiceLog = _mapper.Map<InvoiceRequestModel, InvoiceLog>(log);
            invoiceLog.RestProductId = rest.Id;
            _invoiceLogRepository.Add(invoiceLog);
            _invoiceLogRepository.SaveChanges();

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> Sale(SaleRequestModel log)
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

            _restProductRepository.SaveChanges();
            _clientRepository.SaveChanges();

            var saleLog = _mapper.Map<SaleRequestModel, SaleLog>(log);
            _saleLogRepository.Add(saleLog);
            _saleLogRepository.SaveChanges();
            
            _restProductRepository.SaveChanges();
            _clientRepository.SaveChanges();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }

        return new OkResult();
    }

    public async Task<Result> GetListOfRestProductsByCategory(int categoryId)
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

    public async Task<Result> GetRestsByProduct(int productId)
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

    public async Task<Result> GetRestByFilter(RestFilterRequestModel model)
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
}
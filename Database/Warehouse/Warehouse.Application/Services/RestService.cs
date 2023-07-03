using AutoMapper;
using Warehouse.Application.Common.Interfaces.Repositories;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.RequestModels;
using Warehouse.Application.ResponseModels;
using WareHouse.Core.Models;

namespace Warehouse.Application.Services;

public class RestService:IRestService
{
    private readonly IMapper _mapper;
    private readonly IRestRepository _restRepository;
    private readonly IInvoiceLogRepository _invoiceLogRepository;
    private readonly ISaleLogRepository _saleLogRepository;

    public RestService(IMapper mapper, IRestRepository restRepository, ISaleLogRepository saleLogRepository, IInvoiceLogRepository invoiceLogRepository)
    {
        _mapper = mapper;
        _restRepository = restRepository;
        _saleLogRepository = saleLogRepository;
        _invoiceLogRepository = invoiceLogRepository;
    }

    public List<RestResponseModel> GetRests()
    {
        return _restRepository.GetIEnumerable().Select(s=>_mapper.Map<Rest,RestResponseModel>(s)).ToList();
    }

    public void Invoice(InvoiceRequestModel model)
    {
        var rest = _restRepository.GetIEnumerable()
            .FirstOrDefault(s => s.ProductId == model.ProductId && model.Price == s.Price);
        if (rest==null)
        {
            rest = new Rest
            {
                Id = 0,
                Quantity = 0,
                Price = model.Price,
                ProductId = model.ProductId,
                Product = null!
            };
            _restRepository.Add(rest);
        }

        rest.Quantity += model.Quantity;
        _restRepository.Save();
        
        var invoiceLog = new InvoiceLog
        {
            Id = 0,
            DateTime = DateTime.Now,
            Quantity = model.Quantity,
            Price = model.Price,
            RestId = rest.Id,
            Rest = null!
        };
        
        _invoiceLogRepository.Add(invoiceLog);
        _invoiceLogRepository.Save();
    }

    public void Sale(SaleRequestModel model)
    {
        var rest = _restRepository.GetIEnumerable()
            .FirstOrDefault(s => s.ProductId == model.ProductId && model.Price == s.Price);
        if (rest != null)
        {
            rest.Quantity -= model.Quantity;
            _restRepository.Save();
            
            
            var saleLog = new SaleLog
            {
                Id = 0,
                Price = model.Price,
                DateTime = DateTime.Now,
                Quantity = model.Quantity,
                RestId = rest.Id,
                Rest = null!,
                ClientId = model.ClientId,
                Client = null!
            };

            _saleLogRepository.Add(saleLog);
            _saleLogRepository.Save();
        }
    }
}
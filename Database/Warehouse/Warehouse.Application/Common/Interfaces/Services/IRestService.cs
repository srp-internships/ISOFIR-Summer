using Warehouse.Application.RequestModels;
using Warehouse.Application.ResponseModels;

namespace Warehouse.Application.Common.Interfaces.Services;

public interface IRestService
{
    List<RestResponseModel> GetRests();
    void Invoice(InvoiceRequestModel model);
    void Sale(SaleRequestModel model);
}
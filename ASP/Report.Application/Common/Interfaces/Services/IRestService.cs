using Report.Application.RequestModels;
using Report.Core.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IRestService
{
    public Task<Result> Invoice(InvoiceRequestModel log);
    public Task<Result> Sale(SaleRequestModel log);
    public Task<Result> GetListOfRestProductsByCategory(int categoryId);
    public Task<Result> GetRestsByProduct(int productId);

    public Task<Result> GetRestByFilter(RestFilterRequestModel model);
}

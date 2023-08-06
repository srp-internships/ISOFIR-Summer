using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IRestService
{
    public Task<Result> InvoiceAsync(InvoiceRequestModel log);
    public Task<Result> SaleAsync(SaleRequestModel log);
    public Task<Result> GetListOfRestProductsByCategoryAsync(int categoryId);
    public Task<Result> GetRestsByProductAsync(int productId, int userId);

    public Task<Result> GetRestByFilterAsync(RestFilterRequestModel model);
    public Task<Result> MoveProductsAsync(List<MoveRequestModel> moveRequestModel);
    public Task<Result> InvoiceFromFileAsync(string path, bool isReplaceMode,int userId);
}
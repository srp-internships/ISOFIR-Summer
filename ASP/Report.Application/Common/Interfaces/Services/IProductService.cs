using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IProductService
{
    public Task<Result> CreateOrUpdate(ProductRequestModel productDto);
    public Result Remove(int id);
    public Task<Result> GetAllAsync();
}
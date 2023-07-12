using Report.Application.RequestModels;
using Report.Core.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface ICategoryService
{
    public Task<Result> CreateOrUpdate(CategoryRequestModel categoryDto);
    public Result Remove(int id);
    public Task<Result> GetAllAsync();
}
using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface ICategoryService
{
    public Task<Result> CreateOrUpdateAsync(CategoryRequestModel categoryDto);
    public Task<Result> RemoveAsync(int id);
    public Task<Result> GetAllAsync();
}
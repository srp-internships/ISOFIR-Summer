using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IRateService
{
    public Task<Result> CreateAsync(decimal oneDollarIs, int userId);
    public Task<Result> RemoveAsync(int id);
    public Task<Result> GetAllAsync(int userId);
    public Task<Result> GetLastAsync(int userId);
}
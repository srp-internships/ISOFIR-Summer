using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IReasonService
{
    public Task<Result> CreateOrUpdateAsync(ReasonRequestModel reasonDto);
    public Task<Result> RemoveAsync(int id);
    public Task<Result> GetAllAsync();
}
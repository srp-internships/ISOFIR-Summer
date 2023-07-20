using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface ICashBoxService
{
    public Task<Result> CreateOrUpdateAsync(CashBoxRequestModel cashBoxDto);
    public Task<Result> GetCashBoxCashAsync(int cashBoxId);
    public Task<Result> RemoveAsync(int id);
    public Task<Result> GetAllAsync();
}
using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IFirmService
{
    public Task<Result> CreateOrUpdateAsync(FirmRequestModel firmDto);
    public Task<Result> RemoveAsync(int id);
    public Task<Result> GetAllAsync(int userId);
}
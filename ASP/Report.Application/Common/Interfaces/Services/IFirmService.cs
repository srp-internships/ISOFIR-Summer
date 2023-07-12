using Report.Application.RequestModels;
using Report.Core.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IFirmService
{
    public Task<Result> CreateOrUpdate(FirmRequestModel firmDto);
    public Result Remove(int id);
    public Task<Result> GetAllAsync();
}
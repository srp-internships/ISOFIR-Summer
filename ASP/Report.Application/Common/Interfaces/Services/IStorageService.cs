using Report.Application.RequestModels;
using Report.Core.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IStorageService
{
    public Task<Result> CreateOrUpdate(StorageRequestModel storageDto);
    public Result Remove(int id);
    public Task<Result> GetAllAsync();
}
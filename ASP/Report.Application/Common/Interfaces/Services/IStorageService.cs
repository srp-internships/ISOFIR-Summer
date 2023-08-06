using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IStorageService
{
    public Task<Result> CreateOrUpdateAsync(StorageRequestModel storageDto);
    public Task<Result> RemoveAsync(int id);
    public Task<Result> GetAllAsync(int userId);
    public Task<Result> GetStorageRestsAsync(int storageId);
}
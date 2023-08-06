using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IStorageRepository : IRepository<Storage>
{
    Task<RestProduct> GetRestByProductIdAsync(RestProduct fromRest, int toStorageId);
    Task<List<RestProduct>> GetStorageRestsAsync(int storageId);
    Task<int?> GetIdByNameAsync(string name);
    Task<List<Storage>> GetStoragesWithRestsAsync(int userId);
}
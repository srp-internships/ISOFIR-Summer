using Report.Core.ActionResults;
using Report.Core.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IRestProductRepository:IRepository<RestProduct>
{
    Task<List<RestProduct>> GetRestByCategory(int categoryId);
    Task<List<RestProduct>> GetByStorageFirmProductIds(int modelProductId, int modelStorageId, int modelFirmId);
}
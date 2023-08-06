using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IRestProductRepository : IRepository<RestProduct>
{
    Task<List<RestProduct>> GetRestByCategory(int categoryId);

    Task<List<RestProduct>> GetByStorageFirmProductIds(int modelProductId, int modelStorageId, int modelFirmId,
        int userId);

    Task<List<RestProduct>> GetRestByProductIdAsync(int productId, int userId);

    Task<RestProduct> GetRestForInvoiceAsync(int productId, int storageId, decimal priceUsd,
        decimal priceTjs, int userId);
}
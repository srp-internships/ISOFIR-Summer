using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Core.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class RestProductProductRepository:Repository<RestProduct>, IRestProductRepository
{
    public RestProductProductRepository(DataContext context) : base(context)
    {
    }
    
    public Task<List<RestProduct>> GetRestByCategory(int categoryId)
    {
        return Context.RestProducts.Include(s => s.Product)
            .Where(s => s.Product != null && s.Product.CategoryId == categoryId).ToListAsync();
    }

    public Task<List<RestProduct>> GetByStorageFirmProductIds(int productId, int storageId, int firmId)
    {
        return Context.RestProducts.Include(s => s.InvoiceLogs).Where(s =>
                s.InvoiceLogs != null && s.StorageId == storageId && s.ProductId == productId && s.InvoiceLogs.Any(i => i.FirmId == firmId))
            .ToListAsync();
    }
}
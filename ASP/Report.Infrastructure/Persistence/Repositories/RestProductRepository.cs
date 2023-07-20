using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class RestProductRepository:Repository<RestProduct>, IRestProductRepository
{
    public RestProductRepository(DataContext context) : base(context)
    {
    }

    public new Task<RestProduct?> GetByIdAsync(int id)
    {
        return Set.Include(s => s.Product).FirstOrDefaultAsync(s => s.Id == id);
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
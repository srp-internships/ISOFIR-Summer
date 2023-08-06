using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class RestProductRepository : Repository<RestProduct>, IRestProductRepository
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

    public Task<List<RestProduct>> GetByStorageFirmProductIds(int productId, int storageId, int firmId, int userId)
    {
        var query = Context.RestProducts
            .Include(s => s.Storage)
            .Include(s => s.Product)
            .Include(s => s.InvoiceLogs)
            .Where(s => s.UserId == userId);
        if (productId > 0) query = query.Where(s => s.ProductId == productId);

        if (storageId > 0) query = query.Where(s => s.StorageId == storageId);

        if (firmId > 0) query = query.Where(s => s.InvoiceLogs != null && s.InvoiceLogs.Any(i => i.FirmId == firmId));

        return query.ToListAsync();
    }

    public Task<List<RestProduct>> GetRestByProductIdAsync(int productId, int userId)
    {
        return Context.RestProducts.Include(s => s.Storage).Where(s => s.ProductId == productId && s.UserId == userId)
            .ToListAsync();
    }

    public async Task<RestProduct> GetRestForInvoiceAsync(int productId, int storageId, decimal priceUsd,
        decimal priceTjs, int userId)
    {
        var rest = await Context.RestProducts.FirstOrDefaultAsync(s =>
            s.ProductId == productId && s.StorageId == storageId && s.InvoicePriceUsd == priceUsd &&
            s.InvoicePriceTjs == priceTjs &&
            s.UserId == userId);

        if (rest != null) return rest;

        rest = new RestProduct
        {
            ProductId = productId,
            StorageId = storageId,
            InvoicePriceUsd = priceUsd,
            InvoicePriceTjs = priceTjs,
            UserId = userId
        };
        await Context.RestProducts.AddAsync(rest);
        await Context.SaveChangesAsync();

        return rest;
    }
}
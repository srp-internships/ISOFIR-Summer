using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class StorageRepository:Repository<Storage>,IStorageRepository
{
    public StorageRepository(DataContext context) : base(context)
    {
    }

    public async Task<RestProduct> GetRestByProductIdAsync(RestProduct fromRest, int toStorageId)
    {
        var rest = await Context.RestProducts.FirstOrDefaultAsync(s =>
            s.ProductId == fromRest.ProductId && s.StorageId == toStorageId &&
            s.InvoicePriceUsd == fromRest.InvoicePriceUsd);
        
        if (rest != null) return rest;
        rest = new RestProduct
        {
            StorageId = toStorageId,
            ProductId = fromRest.ProductId
        };
        
        await Context.RestProducts.AddAsync(rest);
        await Context.SaveChangesAsync();

        return rest;
    }

    public async Task<List<RestProduct>> GetStorageRestsAsync(int storageId)
    {
        var storage = await Context.Storages.Include(s => s.RestProducts).ThenInclude(p=>p.Product).FirstOrDefaultAsync(s => s.Id == storageId);
        return storage is { RestProducts: not null } ? storage.RestProducts.ToList() : new List<RestProduct>();
    }

    public async Task<int?> GetIdByNameAsync(string name)
    {
        
        var res = await Context.Storages.FirstOrDefaultAsync(s=>s.Name.ToLower().Trim()==name.Trim().ToLower());
        return res?.Id;
    }
}
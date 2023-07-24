using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class ProductRepository:Repository<Product>, IProductRepository
{
    public ProductRepository(DataContext context) : base(context)
    {
    }

    public new Task<List<Product>> GetAllAsync()
    {
        return Set.Include(s => s.Category).ToListAsync();
    }

    public async Task<int?> GetIdByNameAsync(string name)
    {
        var res = await Context.Products.FirstOrDefaultAsync(s=>s.Name.ToLower().Trim()==name.Trim().ToLower());
        return res?.Id;
    }
}
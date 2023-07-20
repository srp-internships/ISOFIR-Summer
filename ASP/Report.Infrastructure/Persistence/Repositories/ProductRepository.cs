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
}
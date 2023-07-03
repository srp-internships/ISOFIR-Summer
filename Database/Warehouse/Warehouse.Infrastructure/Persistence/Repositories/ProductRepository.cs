using Warehouse.Application.Common.Interfaces.Repositories;
using WareHouse.Core.Models;
using Warehouse.Infrastructure.Persistence.Database;

namespace Warehouse.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public void Remove(int id)
    {
        var old = _context.Products.FirstOrDefault(s => s.Id == id);
        if (old != null)
        {
            _context.Remove(old);
        }
    }

    public IEnumerable<Product> GetIEnumerable()
    {
        return _context.Products;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}
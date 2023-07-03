using WareHouse.Core.Models;

namespace Warehouse.Application.Common.Interfaces.Repositories;

public interface IProductRepository
{
    void Add(Product product);
    void Remove(int id);
    IEnumerable<Product> GetIEnumerable();
    
    int Save();
}
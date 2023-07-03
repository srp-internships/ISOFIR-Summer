using WareHouse.Core.Models;

namespace Warehouse.Application.Common.Interfaces.Repositories;

public interface IRestRepository
{
    void Add(Rest rest);
    void Remove(int id);
    IEnumerable<Rest> GetIEnumerable();
    
    int Save();
}
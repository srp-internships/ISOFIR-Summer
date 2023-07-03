using WareHouse.Core.Models;

namespace Warehouse.Application.Common.Interfaces.Repositories;

public interface IClientRepository
{
    void Add(Client client);
    void Remove(int id);
    IEnumerable<Client> GetIEnumerable();
    
    int Save();
}
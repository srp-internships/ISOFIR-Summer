using Warehouse.Application.Common.Interfaces.Repositories;
using WareHouse.Core.Models;
using Warehouse.Infrastructure.Persistence.Database;

namespace Warehouse.Infrastructure.Persistence.Repositories;

public class ClientRepository:IClientRepository
{
    private readonly DataContext _context;

    public ClientRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(Client client)
    {
        _context.Clients.Add(client);
    }

    public void Remove(int id)
    {
        var old = _context.Clients.FirstOrDefault(s => s.Id == id);
        if (old != null)
            _context.Clients.Remove(old);
    }

    public IEnumerable<Client> GetIEnumerable()
    {
        return _context.Clients;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}
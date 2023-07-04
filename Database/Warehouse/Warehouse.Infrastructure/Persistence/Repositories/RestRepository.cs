using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common.Interfaces.Repositories;
using WareHouse.Core.Models;
using Warehouse.Infrastructure.Persistence.Database;

namespace Warehouse.Infrastructure.Persistence.Repositories;

public class RestRepository:IRestRepository
{
    private readonly DataContext _context;

    public RestRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(Rest rest)
    {
        _context.Rests.Add(rest);
    }

    public void Remove(int id)
    {
        var old = _context.Rests.FirstOrDefault(s => s.Id == id);
        if (old!=null)
        {
            _context.Remove(old);
        }
    }

    public IEnumerable<Rest> GetIEnumerable()
    {
        return _context.Rests.Include(s=>s.Product);
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}
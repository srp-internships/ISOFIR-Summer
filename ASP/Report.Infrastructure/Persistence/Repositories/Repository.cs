using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Core.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class Repository<TEntity>:IRepository<TEntity> where TEntity :BaseModel
{
    protected readonly DbSet<TEntity> Set;
    protected readonly DataContext Context;

    public Repository(DataContext context)
    {
        Set = context.Set<TEntity>();
        Context = context;
    }


    public Task<TEntity?> GetByIdAsync(int id)
    {
        return Set.FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task<bool> ExistByIdAsync(int id)
    {
        return Set.AnyAsync(s => s.Id == id);
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return Set.ToListAsync();
    }
    
    public void Remove(int id)
    {
        var entity = Set.First(s => s.Id == id);
        Set.Remove(entity);
    }


    public void Add(TEntity entity)
    {
        Set.Add(entity);
    }

    public int SaveChanges()
    {
        return Context.SaveChanges();
    }
}
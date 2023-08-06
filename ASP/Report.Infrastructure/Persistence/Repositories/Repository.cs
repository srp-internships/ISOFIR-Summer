using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntitiesModel
{
    protected readonly DataContext Context;
    protected readonly DbSet<TEntity> Set;

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

    public Task<List<TEntity>> GetAllAsync(int userId)
    {
        return Set.Where(s => s.UserId == userId).ToListAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var entity = await Set.FirstAsync(s => s.Id == id);
        Set.Remove(entity);
    }


    public async Task AddAsync(TEntity entity)
    {
        await Set.AddAsync(entity);
    }

    public Task<int> SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }
}
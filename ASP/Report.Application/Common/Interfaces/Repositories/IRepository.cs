using System.Linq.Expressions;
using Report.Core.ActionResults;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IRepository<TEntity>
{
    public Task<TEntity?> GetByIdAsync(int id);
    public Task<bool> ExistByIdAsync(int id);
    public Task<List<TEntity>> GetAllAsync();
    public void Remove(int id);
    public void Add(TEntity entity);
    public int SaveChanges();
}
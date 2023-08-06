namespace Report.Application.Common.Interfaces.Repositories;

public interface IRepository<TEntity>
{
    public Task<TEntity?> GetByIdAsync(int id);
    public Task<bool> ExistByIdAsync(int id);
    public Task<List<TEntity>> GetAllAsync(int userId);
    public Task RemoveAsync(int id);
    public Task AddAsync(TEntity entity);
    public Task<int> SaveChangesAsync();
}
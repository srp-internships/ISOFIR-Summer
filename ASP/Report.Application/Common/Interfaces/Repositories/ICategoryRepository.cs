using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface ICategoryRepository:IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}
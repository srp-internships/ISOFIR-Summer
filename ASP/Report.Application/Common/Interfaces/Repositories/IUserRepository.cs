using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByLoginAsync(string login);
}
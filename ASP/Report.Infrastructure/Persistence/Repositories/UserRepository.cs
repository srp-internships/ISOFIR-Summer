using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly DbSet<User> _set;


    public UserRepository(DataContext context)
    {
        _context = context;
        _set = context.Users;
    }

    public Task<User?> GetByLoginAsync(string login)
    {
        return _set.FirstOrDefaultAsync(l => string.Equals(l.Login, login, StringComparison.CurrentCultureIgnoreCase));
    }


    public Task<User?> GetByIdAsync(int id)
    {
        return _set.FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task<bool> ExistByIdAsync(int id)
    {
        return _set.AnyAsync(s => s.Id == id);
    }

    public Task<List<User>> GetAllAsync(int userId)
    {
        return _set.ToListAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var entity = await _set.FirstAsync(s => s.Id == id);
        _set.Remove(entity);
    }


    public async Task AddAsync(User entity)
    {
        await _set.AddAsync(entity);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class RateRepository : Repository<Rate>, IRateRepository
{
    public RateRepository(DataContext context) : base(context)
    {
    }

    public Task<decimal> GetLastAsync(int userId)
    {
        return Context.Rates.OrderByDescending(s => s.DateTime).Where(s => s.UserId == userId)
            .Select(s => s.OneDollarIs).FirstOrDefaultAsync();
    }

    public new Task<List<Rate>> GetAllAsync(int userId)
    {
        return Context.Rates.OrderByDescending(s => s.DateTime).Where(s => s.UserId == userId).ToListAsync();
    }
}
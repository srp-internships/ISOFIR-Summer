using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class SaleLogRepository : Repository<SaleLog>, ISaleLogRepository
{
    public SaleLogRepository(DataContext context) : base(context)
    {
    }

    public Task<List<SaleLog>> GetClientHistoryAsync(int clientId, int userId)
    {
        return Context.SaleLogs.Include(s => s.RestProduct.Storage).Include(s => s.RestProduct.Product)
            .Include(s => s.Client).OrderByDescending(s => s.DateTime)
            .Where(l => l.ClientId == clientId && l.UserId == userId).ToListAsync();
    }

    public Task<List<SaleLog>> GetToDaySalesAsync(int userId)
    {
        var now = DateTime.Now;
        return Context.SaleLogs.Include(s => s.RestProduct)
            .Where(s => s.UserId == userId && s.DateTime.Year==now.Year && s.DateTime.Month==now.Month && s.DateTime.Day==now.Day).ToListAsync();
    }
}
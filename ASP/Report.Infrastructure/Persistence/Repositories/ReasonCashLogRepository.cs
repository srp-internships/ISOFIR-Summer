using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class ReasonCashLogRepository : Repository<ReasonCashLog>, IReasonCashLogRepository
{
    public ReasonCashLogRepository(DataContext context) : base(context)
    {
    }

    public new Task<List<ReasonCashLog>> GetAllAsync()
    {
        return Context.ReasonCashLogs.Include(s => s.Reason).Include(s => s.CashBox).ToListAsync();
    }
}
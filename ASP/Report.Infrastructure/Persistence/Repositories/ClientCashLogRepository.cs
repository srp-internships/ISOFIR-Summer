using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class ClientCashLogRepository:Repository<ClientCashLog>,IClientCashLogRepository
{
    public ClientCashLogRepository(DataContext context) : base(context)
    {
    }

    public new Task<List<ClientCashLog>> GetAllAsync()
    {
        return Context.ClientCashLogs.Include(s => s.Client).Include(s => s.CashBox).OrderBy(s=>s.DateTime).ToListAsync();
    }
}
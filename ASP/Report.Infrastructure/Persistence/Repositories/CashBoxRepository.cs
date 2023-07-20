using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class CashBoxRepository:Repository<CashBox>,ICashBoxRepository
{
    public CashBoxRepository(DataContext context) : base(context)
    { }
}
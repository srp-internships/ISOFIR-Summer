using Report.Application.Common.Interfaces.Repositories;
using Report.Core.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class SaleLogRepository:Repository<SaleLog>, ISaleLogRepository
{
    public SaleLogRepository(DataContext context) : base(context)
    {
    }
}
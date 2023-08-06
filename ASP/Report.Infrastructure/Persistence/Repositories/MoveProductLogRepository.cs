using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class MoveProductLogRepository : Repository<MoveProductLog>, IMoveProductLogRepository
{
    public MoveProductLogRepository(DataContext context) : base(context)
    {
    }
}
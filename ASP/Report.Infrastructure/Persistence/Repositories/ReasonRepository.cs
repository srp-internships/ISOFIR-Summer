using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class ReasonRepository : Repository<Reason>, IReasonRepository
{
    public ReasonRepository(DataContext context) : base(context)
    {
    }
}
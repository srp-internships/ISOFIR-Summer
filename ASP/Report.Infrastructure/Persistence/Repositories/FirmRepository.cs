using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class FirmRepository:Repository<Firm>, IFirmRepository
{
    public FirmRepository(DataContext context) : base(context)
    {
    }
}
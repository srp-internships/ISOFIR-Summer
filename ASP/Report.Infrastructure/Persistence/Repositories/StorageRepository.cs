using Report.Application.Common.Interfaces.Repositories;
using Report.Core.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class StorageRepository:Repository<Storage>,IStorageRepository
{
    public StorageRepository(DataContext context) : base(context)
    {
    }
}

using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class ClientRepository:Repository<Client>, IClientRepository
{
    public ClientRepository(DataContext context) : base(context)
    {
    }
}
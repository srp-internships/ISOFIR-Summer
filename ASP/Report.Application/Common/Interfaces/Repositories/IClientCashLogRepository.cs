using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IClientCashLogRepository : IRepository<ClientCashLog>
{
    Task<List<ClientCashLog>> GetByClientIdAsync(int clientId);
}
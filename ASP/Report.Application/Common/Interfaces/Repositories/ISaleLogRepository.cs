using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface ISaleLogRepository : IRepository<SaleLog>
{
    Task<List<SaleLog>> GetClientHistoryAsync(int clientId, int userId);
    Task<List<SaleLog>> GetToDaySalesAsync(int userId);
}
using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IRateRepository : IRepository<Rate>
{
    Task<decimal> GetLastAsync(int userId);
}
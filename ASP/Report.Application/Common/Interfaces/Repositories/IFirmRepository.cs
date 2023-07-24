using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Repositories;

public interface IFirmRepository:IRepository<Firm>
{
    public Task<int?> GetIdByNameAsync(string name);
}
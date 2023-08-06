using Microsoft.EntityFrameworkCore;
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class FirmRepository : Repository<Firm>, IFirmRepository
{
    public FirmRepository(DataContext context) : base(context)
    {
    }

    public async Task<int?> GetIdByNameAsync(string name)
    {
        var res = await Context.Firms.FirstOrDefaultAsync(s => s.Name.ToLower().Trim() == name.Trim().ToLower());
        return res?.Id;
    }
}
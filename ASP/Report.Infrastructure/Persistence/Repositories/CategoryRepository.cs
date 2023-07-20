
using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class CategoryRepository:Repository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext context) : base(context)
    {
    }
}
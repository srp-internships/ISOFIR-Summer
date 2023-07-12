using Microsoft.Extensions.DependencyInjection;
using Report.Application.Common.Interfaces.Repositories;
using Report.Infrastructure.Persistence.DataBase;
using Report.Infrastructure.Persistence.Repositories;

namespace Report.Infrastructure;

public static class DependencyInitializer
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IFirmRepository, FirmRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IStorageRepository, StorageRepository>();
        services.AddScoped<IInvoiceLogRepository, InvoiceLogRepository>();
        services.AddScoped<ISaleLogRepository, SaleLogRepository>();
        services.AddScoped<IRestProductRepository, RestProductProductRepository>();
        services.AddScoped<DataContext>();
    }
}
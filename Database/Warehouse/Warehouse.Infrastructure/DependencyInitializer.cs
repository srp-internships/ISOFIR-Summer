using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common.Interfaces.Repositories;
using Warehouse.Infrastructure.Persistence.Database;
using Warehouse.Infrastructure.Persistence.Repositories;

namespace Warehouse.Infrastructure;

public static class DependencyInitializer
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IInvoiceLogRepository, InvoiceLogRepository>();
        services.AddScoped<ISaleLogRepository, SaleRepository>();
        services.AddScoped<IRestRepository, RestRepository>();
        services.AddScoped<DataContext>();
    }
}
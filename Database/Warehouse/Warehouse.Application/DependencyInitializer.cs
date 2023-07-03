using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.Mappers;
using Warehouse.Application.Services;

namespace Warehouse.Application;

public static class DependencyInitializer
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfigurations).Assembly);
        services.AddScoped<IRestService, RestService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IProductService, ProductService>();
    }
}
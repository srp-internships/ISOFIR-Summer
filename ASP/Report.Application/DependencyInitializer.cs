using Microsoft.Extensions.DependencyInjection;
using Report.Application.Common.Interfaces.Services;
using Report.Application.Mappers;
using Report.Application.Services;

namespace Report.Application;

public static class DependencyInitializer
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfigurations).Assembly);

        services.AddScoped<IReasonService, ReasonService>();
        services.AddScoped<IReasonCashBoxService, ReasonCashBoxService>();
        services.AddScoped<ICashBoxService, CashBoxService>();
        services.AddScoped<IClientCashBoxService, ClientCashBoxService>();
        services.AddScoped<IReasonCashBoxService, ReasonCashBoxService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IFirmService, FirmService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IRestService, RestService>();
        services.AddScoped<IRateService, RateService>();

        services.AddScoped<IAuthService, AuthService>();
    }
}
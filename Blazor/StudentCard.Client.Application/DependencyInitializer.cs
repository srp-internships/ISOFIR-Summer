using Microsoft.Extensions.DependencyInjection;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Client.Application.Services;

namespace StudentCard.Client.Application;

public static class DependencyInitializer
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAgentService, AgentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IPayService, PayService>();
    }
}
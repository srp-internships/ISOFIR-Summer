using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudentCard.Application.Infrastructure.DataBase;
using StudentCard.Application.Interfaces.Services;
using StudentCard.Application.Mappers;
using StudentCard.Application.Services;

namespace StudentCard.Application;

public static class DependencyInitializer
{
    public static void AddApplication(this IServiceCollection services, string connectionString,string token)
    {
        DataContext.ConnectionString = connectionString;
        AuthService.Token = token;
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(System.Text.Encoding.UTF8
                            .GetBytes(token)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPayService, PayService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<DataContext>();
        services.AddAutoMapper(typeof(AutoMapperConfigurations).Assembly);
    }
}
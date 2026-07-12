using Hospital.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Hospital.Infrastructure.Persistence;
using Hospital.Infrastructure.Interceptors;
using Hospital.Infrastructure.Repositories;

namespace Hospital.API.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddScoped<AuditInterceptor>();

        return services;
    }

    public static IServiceCollection AddDbPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var interceptorAudit = serviceProvider.GetService<AuditInterceptor>()!;

            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseLowerCaseNamingConvention()
                .AddInterceptors(interceptorAudit);
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
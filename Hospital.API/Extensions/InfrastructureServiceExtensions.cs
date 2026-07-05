using Hospital.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Hospital.Infrastructure.Persistence;
using Hospital.Infrastructure.Interceptors;
using Hospital.Infrastructure.Repositories;

namespace Hospital.API.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Registrar el interceptor de auditoria
        services.AddScoped<AuditInterceptor>();

        // Configurar DbContext PostgreSQL
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            // Resolvemos el interceptor desde el contenedor para que pueda usar ICurrentUserService internamente
            var auditInterceptor = sp.GetRequiredService<AuditInterceptor>();
            options.AddInterceptors(auditInterceptor);
        });

        // Registrar Repositorio Genérico y Unit of Work
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
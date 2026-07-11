using Asp.Versioning;
using Hospital.Api.Identity;
using Hospital.API.Middleware;
using Hospital.Domain.Interfaces;

namespace Hospital.API.Extensions;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // 1. Necesario para leer el contexto de la petición web y extraer el Token/Usuario
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // 2. Manejo Global de Excepciones         
        services.AddExceptionHandler<GlobalExceptionHandler>();

        // Requerido para que el framework formatee correctamente las respuestas HTTP de error
        services.AddProblemDetails();

        return services;
    }

    public static IServiceCollection AddScalarConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        .AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddOpenApi();

        return services;
    }
}
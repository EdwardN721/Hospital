using System.Reflection;
using FluentValidation;
using Hospital.API.Middleware;
using Hospital.Application.Interfaces;
using Hospital.Application.Services;
using Hospital.Domain.Interfaces;

namespace Hospital.API.Extensions;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // 1. Necesario para leer el contexto de la petición web y extraer el Token/Usuario
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // 2. Manejo Global de Excepciones (.NET 8/10)         
        services.AddExceptionHandler<GlobalExceptionHandler>();

        // Requerido para que el framework formatee correctamente las respuestas HTTP de error
        services.AddProblemDetails();

        return services;
    }

    public static IServiceCollection AddValidatorConfig(this IServiceCollection services)
    {
        // Escanea este ensamblado y registra todas las clases que hereden de AbstractValidator
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Registramos nuestros servicios de negocio
        services.AddScoped<IPatientService, PatientService>();

        return services;
    }
}
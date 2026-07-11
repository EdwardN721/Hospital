using System.Reflection;
using FluentValidation;
using Hospital.Application.Interfaces;
using Hospital.Application.Services;

namespace Hospital.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Agregar Fluent Validations -> Validadores
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Servicies
        services.AddScoped<IPatientService, PatientService>();
        

        return services;
    }
}
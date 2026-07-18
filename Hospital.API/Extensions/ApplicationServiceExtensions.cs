using System.Reflection;
using FluentValidation;
using Hospital.Application.DTOs.Requests.Patients;
using Hospital.Application.Interfaces;
using Hospital.Application.Services;

namespace Hospital.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Agregar Fluent Validations -> Validadores
        services.AddValidatorsFromAssembly(typeof(CreatePatientRequest).Assembly);

        // Servicies
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<ISpecialtyService, SpecialtyService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IBedService, BedService>();
        services.AddScoped<IFloorService, FloorService>();
        services.AddScoped<IPrescriptionService, PrescriptionService>();
        services.AddScoped<IAdmissionService, AdmissionService>();
        services.AddScoped<IInvoiceService, InvoiceService>();

        return services;
    }
}
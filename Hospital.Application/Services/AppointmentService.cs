using FluentValidation;
using FluentValidation.Results;
using Hospital.Application.DTOs.Requests.Appointments;
using Hospital.Application.DTOs.Responses.Appointments;
using Hospital.Application.Interfaces;
using Hospital.Application.Mappers;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Clinical;
using Hospital.Domain.Models.Core;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AppointmentService> _logger;
    private readonly IValidator<CreateAppointmentRequest> _createValidator;

    public AppointmentService(IUnitOfWork unitOfWork, IValidator<CreateAppointmentRequest> createValidator, ILogger<AppointmentService> logger)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _logger = logger;
    }

    public async Task<AppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest request)
    {
        _logger.LogInformation("Agendando cita para el paciente {PatientId}", request.PatientId);

        ValidationResult? validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Error al ingresar los datos.");
            var errors = validationResult.Errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                                                .ToDictionary(g => g.Key, g => g.ToArray());
            throw new AppValidationException(errors);
        }

        // Validar si paciente y doctor existen
        var patient = await _unitOfWork.Pacientes.GetByIdAsync(request.PatientId);
        if (patient == null) throw new BusinessRuleException("El paciente especificado no existe.");

        var doctor = await _unitOfWork.Doctores.GetByIdAsync(request.DoctorId);
        if (doctor == null) throw new BusinessRuleException("El doctor especificado no existe.");

        // *Nota: Aquí un Senior agregaría una consulta para verificar que el Doctor 
        // no tenga ya otra cita a esa misma hora (request.ScheduledTime) 
        // para evitar que la Base de Datos lance la excepción del CONSTRAINT UQ_Doctor_Schedule.

        // 3. Mapeo y Persistencia
        Appointment appointment = request.MapToEntity();
        
        await _unitOfWork.Citas.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Cita agendada con éxito.");
        return appointment.MapToDto();
    }

    public async Task ActualizarAppointmentAsync(Guid idAppointment, UpdateAppointmentRequest request)
    {
        Appointment appointment = await ObtenerEntidadPorId(idAppointment);

        // Validar el status permitido
        var validStatuses = new[] { "Pending", "Confirmed", "Completed", "Cancelled" };
        if (!validStatuses.Contains(request.Status))
        {
            throw new BusinessRuleException($"El estado '{request.Status}' no es válido.");
        }

        appointment.UpdateEntity(request);
        _unitOfWork.Citas.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task EliminarAppointmentAsync(Guid idAppointment)
    {
        Appointment appointment = await ObtenerEntidadPorId(idAppointment);
        
        if (appointment.Status == "Completed")
        {
             throw new BusinessRuleException("No se puede eliminar una cita que ya fue completada.");
        }

        _unitOfWork.Citas.Delete(appointment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<AppointmentResponse> ObtenerPorIdAsync(Guid idAppointment)
    {
        Appointment appointment = await ObtenerEntidadPorId(idAppointment);
        return appointment.MapToDto();
    }

    public async Task<IEnumerable<AppointmentResponse>> ObtenerTodasAsync()
    {
        var appointments = await _unitOfWork.Citas.GetAllAsync();
        return appointments.MapToDto();
    }

    private async Task<Appointment> ObtenerEntidadPorId(Guid idAppointment)
    {
        Appointment? appointment = await _unitOfWork.Citas.GetByIdAsync(idAppointment);
        if (appointment == null) throw new NotFoundException(nameof(Appointment), idAppointment);
        return appointment;
    }
}
using FluentValidation;
using Hospital.Application.DTOs.Requests.Consultations;
using Hospital.Application.DTOs.Responses.Consultations;
using Hospital.Application.Interfaces;
using Hospital.Application.Mappers;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Clinical;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Services;

public class ConsultationService : IConsultationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateConsultationRequest> _createValidator;
    private readonly ILogger<ConsultationService> _logger;

    public ConsultationService(IUnitOfWork unitOfWork, IValidator<CreateConsultationRequest> validator, ILogger<ConsultationService> logger)
    {
        _unitOfWork = unitOfWork;
        _createValidator = validator;
        _logger = logger;
    }

    public async Task<ConsultationResponse> CreateConsultationAsync(CreateConsultationRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                                                .ToDictionary(g => g.Key, g => g.ToArray());
            throw new AppValidationException(errors);
        }

        // REGLA 1: Verificar que la Cita exista y pertenezca a ese paciente/doctor
        var appointment = await _unitOfWork.Citas.GetByIdAsync(request.AppointmentId);
        if (appointment == null) throw new BusinessRuleException("La cita especificada no existe.");
        
        if (appointment.PatientId != request.PatientId || appointment.DoctorId != request.DoctorId)
            throw new BusinessRuleException("Los datos del paciente o doctor no coinciden con la cita agendada.");

        if (appointment.Status == "Completed" || appointment.Status == "Cancelled")
            throw new BusinessRuleException($"No se puede iniciar una consulta sobre una cita con estado: {appointment.Status}");

        // TRABAJO TRANSACCIONAL CON UNIT OF WORK
        // 1. Iniciamos la transacción (Opcional si usas solo SaveChanges, pero buena práctica)
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // 2. Mapear y guardar la Consulta
            Consultation consultation = request.MapToEntity();
            await _unitOfWork.Consultas.AddAsync(consultation);

            // 3. Actualizar la Cita a 'Completed'
            appointment.Status = "Completed";
            _unitOfWork.Citas.Update(appointment);

            // 4. Guardamos todo junto. Si esto tiene éxito, la BD queda consistente.
            await _unitOfWork.CommitTransactionAsync();
            
            _logger.LogInformation("Consulta creada y Cita {AppId} marcada como completada.", appointment.AppointmentId);
            return consultation.MapToDto();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Error al crear la consulta transaccionalmente.");
            throw; // El GlobalExceptionHandler lo atrapará
        }
    }

    public async Task<ConsultationResponse> ObtenerPorIdAsync(Guid idConsultation)
    {
        var consultation = await _unitOfWork.Consultas.GetByIdAsync(idConsultation);
        if (consultation == null) throw new NotFoundException(nameof(Consultation), idConsultation);
        return consultation.MapToDto();
    }
}
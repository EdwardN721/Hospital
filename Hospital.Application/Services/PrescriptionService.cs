using FluentValidation;
using Hospital.Application.DTOs.Requests.Prescriptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Clinical;

namespace Hospital.Application.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePrescriptionRequest> _validator;

    public PrescriptionService(IUnitOfWork unitOfWork, IValidator<CreatePrescriptionRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Guid> CreatePrescriptionAsync(CreatePrescriptionRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new AppValidationException(validationResult.Errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage).ToDictionary(g => g.Key, g => g.ToArray()));

        // REGLA DE NEGOCIO CRÍTICA: Validación Cruzada
        var consultation = await _unitOfWork.Consultas.GetByIdAsync(request.ConsultationId);
        
        if (consultation == null) 
            throw new BusinessRuleException("La consulta asociada no existe.");

        // Seguridad: Garantizar que el doctor no asigne esta receta a un paciente distinto al de la consulta
        if (consultation.PatientId != request.PatientId)
            throw new BusinessRuleException("Incongruencia de datos: El paciente de la receta no coincide con el paciente de la consulta médica.");

        var prescription = new Prescription
        {
            ConsultationId = request.ConsultationId,
            PatientId = request.PatientId,
            Notes = request.Notes
        };

        await _unitOfWork.Prescripciones.AddAsync(prescription);
        await _unitOfWork.SaveChangesAsync();

        return prescription.PrescriptionId;
    }
}
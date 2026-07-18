using FluentValidation;
using Hospital.Application.DTOs.Requests.Consultations;

namespace Hospital.Application.Validators.Consultations;

public class CreateConsultationRequestValidator : AbstractValidator<CreateConsultationRequest>
{
    public CreateConsultationRequestValidator()
    {
        RuleFor(x => x.AppointmentId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        
        // Reglas clínicas basadas en tus constraints de PostgreSQL
        RuleFor(x => x.SystolicPressure)
            .InclusiveBetween(50, 250).When(x => x.SystolicPressure.HasValue)
            .WithMessage("La presión sistólica debe estar entre 50 y 250.");
            
        RuleFor(x => x.DiastolicPressure)
            .InclusiveBetween(30, 150).When(x => x.DiastolicPressure.HasValue)
            .WithMessage("La presión diastólica debe estar entre 30 y 150.");
            
        RuleFor(x => x.Temperature)
            .InclusiveBetween(30.0m, 45.0m).When(x => x.Temperature.HasValue)
            .WithMessage("La temperatura debe estar entre 30.0 y 45.0 °C.");
    }
}
using FluentValidation;
using Hospital.Application.DTOs.Requests.Prescriptions;

namespace Hospital.Application.Validators.Prescriptions;

public class CreatePrescriptionRequestValidator : AbstractValidator<CreatePrescriptionRequest>
{
    public CreatePrescriptionRequestValidator()
    {
        RuleFor(x => x.ConsultationId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.Notes).NotEmpty().WithMessage("La receta debe contener indicaciones o medicamentos.");
    }
}
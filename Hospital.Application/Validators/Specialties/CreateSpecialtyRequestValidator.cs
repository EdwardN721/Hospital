using FluentValidation;
using Hospital.Application.DTOs.Requests.Specialties;

namespace Hospital.Application.Validators.Specialties;

public class CreateSpecialtyRequestValidator : AbstractValidator<CreateSpecialtyRequest>
{
    public CreateSpecialtyRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la especialidad es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");
    }
}
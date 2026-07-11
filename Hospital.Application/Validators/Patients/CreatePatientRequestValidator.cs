using FluentValidation;
using Hospital.Application.DTOs.Requests.Patients;

namespace Hospital.Application.Validators.Patients;

public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
{
    public CreatePatientRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es requerido.")
            .MaximumLength(100).WithMessage("El apellido no puede exceder los 100 caracteres.");

        RuleFor(x => x.DocumentId)
            .NotEmpty().WithMessage("El documento de identidad es requerido.")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.UtcNow).WithMessage("La fecha de nacimiento no puede ser en el futuro.");

        RuleFor(x => x.Gender)
            .Must(g => g == 'M' || g == 'F' || g == 'O')
            .WithMessage("El género debe ser 'M', 'F' u 'O'.");
    }
}
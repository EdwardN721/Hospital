using FluentValidation;
using Hospital.Application.DTOs.Requests.Doctors;

namespace Hospital.Application.Validators.Doctors;

public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
{
    public CreateDoctorRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now).WithMessage("La fecha no puede ser futura.");
        RuleFor(x => x.DocumentId).NotEmpty().MaximumLength(50);
        RuleFor(x => x.SpecialtyId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.MedicalLicense).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Gender).Must(g => g == 'M' || g == 'F' || g == 'O');
    }
}
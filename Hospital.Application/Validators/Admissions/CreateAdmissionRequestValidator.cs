using FluentValidation;
using Hospital.Application.DTOs.Requests.Admissions;

namespace Hospital.Application.Validators.Admissions;

public class CreateAdmissionRequestValidator : AbstractValidator<CreateAdmissionRequest>
{
    public CreateAdmissionRequestValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.BedId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(500);
    }
}
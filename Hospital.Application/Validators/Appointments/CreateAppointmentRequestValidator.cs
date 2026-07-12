using FluentValidation;
using Hospital.Application.DTOs.Requests.Appointments;

namespace Hospital.Application.Validators.Appointments;

public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequest>
{
    public CreateAppointmentRequestValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty().NotEqual(Guid.Empty).WithMessage("El paciente es requerido.");
        RuleFor(x => x.DoctorId).NotEmpty().NotEqual(Guid.Empty).WithMessage("El doctor es requerido.");
        
        RuleFor(x => x.ScheduledTime)
            .GreaterThan(DateTimeOffset.UtcNow)
            .WithMessage("La cita debe ser programada para una fecha y hora futura.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("El motivo de la cita es requerido.")
            .MaximumLength(500);
    }
}
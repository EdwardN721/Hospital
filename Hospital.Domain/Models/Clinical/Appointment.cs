using Hospital.Domain.Models.Core;

namespace Hospital.Domain.Models.Clinical;

public class Appointment : BaseEntity
{
    public Guid AppointmentId { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTimeOffset ScheduledTime { get; set; }
    public string Status { get; set; } = null!;
    public string Reason { get; set; } = null!;

    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Consultation? Consultation { get; set; }
}
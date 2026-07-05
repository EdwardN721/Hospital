using Hospital.Domain.Models.Core;

namespace Hospital.Domain.Models.Clinical;

public class Admission : BaseEntity
{
    public Guid AdmissionId { get; set; }
    public Guid PatientId { get; set; }
    public Guid BedId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime AdmissionDate { get; set; } = DateTime.UtcNow;
    public DateTime? DischargeDate { get; set; }
    public string Reason { get; set; } = null!;

    public Patient Patient { get; set; } = null!;
    public Bed Bed { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
}
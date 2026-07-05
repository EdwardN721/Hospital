using Hospital.Domain.Models.Core;

namespace Hospital.Domain.Models.Clinical;

public class Prescription : BaseEntity
{
    public Guid PrescriptionId { get; set; }
    public Guid ConsultationId { get; set; }
    public Guid PatientId { get; set; }
    public string? Notes { get; set; }

    public Consultation Consultation { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
}
using Hospital.Domain.Models.Core;

namespace Hospital.Domain.Models.Clinical;

public class Consultation : BaseEntity
{
    public Guid ConsultationId { get; set; }
    public Guid? AppointmentId { get; set; } // Puede ser nulo si es consulta de urgencia sin cita
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Notes { get; set; }
    public int? SystolicPressure { get; set; }
    public int? DiastolicPressure { get; set; }
    public int? HeartRate { get; set; }
    public decimal? Temperature { get; set; }

    public Appointment? Appointment { get; set; }
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
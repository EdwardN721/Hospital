using Hospital.Domain.Models.Clinical;

namespace Hospital.Domain.Models.Core;

public class Doctor : Person
{
    public Guid SpecialtyId { get; set; }
    public string MedicalLicense { get; set; } = null!;
    
    public Specialty Specialty { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
using Hospital.Domain.Models.Clinical;

namespace Hospital.Domain.Models.Core;

public class Patient :  Person
{
    public Guid PatientId { get; set; }
    public string? BloodType { get; set; }
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
}
namespace Hospital.Application.DTOs.Requests.Admissions;

public record CreateAdmissionRequest(
    Guid PatientId,
    Guid BedId,
    Guid DoctorId,
    string Reason
);
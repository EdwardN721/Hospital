namespace Hospital.Application.DTOs.Requests.Doctors;

public record CreateDoctorRequest(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    char Gender,
    string DocumentId,
    string? Email,
    Guid SpecialtyId,
    string MedicalLicense
);
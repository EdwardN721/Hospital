namespace Hospital.Application.DTOs.Requests.Doctors;

public record UpdateDoctorRequest(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    char Gender,
    string? Email,
    Guid SpecialtyId,
    string MedicalLicense
);
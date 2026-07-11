namespace Hospital.Application.DTOs.Responses.Patients;

public record PatientResponse(
    Guid PatientId,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    char? Gender,
    string DocumentId,
    string? Email,
    string? BloodType,
    string? EmergencyContactName,
    string? EmergencyContactPhone
);
namespace Hospital.Application.DTOs.Requests.Patients;

public record CreatePatientRequest(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    char Gender,
    string DocumentId,
    string? Email,
    string? BloodType,
    string? EmergencyContactName,
    string? EmergencyContactPhone
);
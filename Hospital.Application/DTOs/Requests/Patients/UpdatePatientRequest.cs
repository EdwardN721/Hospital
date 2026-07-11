namespace Hospital.Application.DTOs.Requests.Patients;

public record UpdatePatientRequest(
    Guid PatientId,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    char Gender,
    string? Email,
    string? BloodType,
    string? EmergencyContactName,
    string? EmergencyContactPhone
);
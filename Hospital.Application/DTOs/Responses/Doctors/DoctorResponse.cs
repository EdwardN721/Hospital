namespace Hospital.Application.DTOs.Responses.Doctors;

public record DoctorResponse(
    Guid DoctorId,
    string FirstName,
    string LastName,
    string MedicalLicense,
    Guid SpecialtyId,
    string SpecialtyName
);
namespace Hospital.Application.DTOs.Responses.Admissions;

public record AdmissionResponse(
    Guid AdmissionId,
    Guid PatientId,
    Guid BedId,
    Guid DoctorId,
    DateTimeOffset AdmissionDate,
    DateTimeOffset? DischargeDate,
    string Reason
);
namespace Hospital.Application.DTOs.Requests.Prescriptions;

public record CreatePrescriptionRequest(
    Guid ConsultationId,
    Guid PatientId,
    string Notes
);
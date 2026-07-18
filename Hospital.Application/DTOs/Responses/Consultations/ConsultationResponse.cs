namespace Hospital.Application.DTOs.Responses.Consultations;

public record ConsultationResponse(
    Guid ConsultationId,
    Guid? AppointmentId,
    DateTime StartTime,
    DateTime? EndTime,
    string? Notes
);
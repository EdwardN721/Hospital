namespace Hospital.Application.DTOs.Requests.Consultations;

public record CreateConsultationRequest(
    Guid AppointmentId,
    Guid PatientId,
    Guid DoctorId,
    DateTime StartTime,
    string? Notes,
    int? SystolicPressure,
    int? DiastolicPressure,
    int? HeartRate,
    decimal? Temperature
);
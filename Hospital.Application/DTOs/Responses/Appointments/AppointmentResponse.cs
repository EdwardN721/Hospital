namespace Hospital.Application.DTOs.Responses.Appointments;

public record AppointmentResponse(
    Guid AppointmentId,
    Guid PatientId,
    Guid DoctorId,
    DateTimeOffset ScheduledTime,
    string Status,
    string Reason
);
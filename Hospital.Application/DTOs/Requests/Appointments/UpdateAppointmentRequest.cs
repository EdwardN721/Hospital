namespace Hospital.Application.DTOs.Requests.Appointments;

public record UpdateAppointmentRequest(
    Guid AppointmentId,
    DateTimeOffset ScheduledTime,
    string Reason,
    string Status
);
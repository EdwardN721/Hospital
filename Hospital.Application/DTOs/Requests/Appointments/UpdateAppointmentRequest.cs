namespace Hospital.Application.DTOs.Requests.Appointments;

public record UpdateAppointmentRequest(
    Guid AppointmentId,
    DateTime ScheduledTime,
    string Reason
);
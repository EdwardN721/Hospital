namespace Hospital.Application.DTOs.Requests.Appointments;

public record CreateAppointmentRequest(
    Guid PatientId,
    Guid DoctorId,
    DateTimeOffset ScheduledTime,
    string Reason
);
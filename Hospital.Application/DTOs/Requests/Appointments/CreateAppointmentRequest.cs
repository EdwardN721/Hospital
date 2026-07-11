namespace Hospital.Application.DTOs.Requests.Appointments;

public record CreateAppointmentRequest(
    Guid PatientId,
    Guid DoctorId,
    DateTime ScheduledTime,
    string Reason
);
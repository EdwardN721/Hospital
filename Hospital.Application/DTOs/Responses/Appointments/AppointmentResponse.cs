namespace Hospital.Application.DTOs.Responses.Appointments;

public record AppointmentResponse(
    Guid AppointmentId,
    Guid PatientId,
    string PatientFullName,
    Guid DoctorId,
    string DoctorFullName,
    DateTime ScheduledTime,
    string Status,
    string Reason
);
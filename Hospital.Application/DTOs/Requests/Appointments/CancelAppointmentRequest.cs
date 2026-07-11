namespace Hospital.Application.DTOs.Requests.Appointments;

public record CancelAppointmentRequest(
    Guid AppointmentId,
    string CancellationReason
);
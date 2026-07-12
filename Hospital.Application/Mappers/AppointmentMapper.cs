using Hospital.Application.DTOs.Requests.Appointments;
using Hospital.Application.DTOs.Responses.Appointments;
using Hospital.Domain.Models.Clinical;

namespace Hospital.Application.Mappers;

public static class AppointmentMapper
{
    public static AppointmentResponse MapToDto(this Appointment appointment)
    {
        return new AppointmentResponse(
            appointment.AppointmentId,
            appointment.PatientId,
            appointment.DoctorId,
            appointment.ScheduledTime,
            appointment.Status,
            appointment.Reason
        );
    }

    public static IEnumerable<AppointmentResponse> MapToDto(this IEnumerable<Appointment>? appointments)
    {
        return appointments?.Select(a => a.MapToDto()) ?? Enumerable.Empty<AppointmentResponse>();
    }

    public static Appointment MapToEntity(this CreateAppointmentRequest request)
    {
        return new Appointment
        {
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            ScheduledTime = request.ScheduledTime,
            Reason = request.Reason,
            Status = "Pending"
        };
    }

    public static void UpdateEntity(this Appointment appointment, UpdateAppointmentRequest request)
    {
        appointment.ScheduledTime = request.ScheduledTime;
        appointment.Status = request.Status;
        appointment.Reason = request.Reason;
    }
}
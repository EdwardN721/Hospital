using Hospital.Application.DTOs.Requests.Appointments;
using Hospital.Application.DTOs.Responses.Appointments;

namespace Hospital.Application.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest request);
    Task<AppointmentResponse> ObtenerPorIdAsync(Guid idAppointment);
    Task<IEnumerable<AppointmentResponse>> ObtenerTodasAsync();
    Task ActualizarAppointmentAsync(Guid idAppointment, UpdateAppointmentRequest request);
    Task EliminarAppointmentAsync(Guid idAppointment);
}
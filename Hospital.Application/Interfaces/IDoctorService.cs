using Hospital.Application.DTOs.Requests.Doctors;
using Hospital.Application.DTOs.Responses.Doctors;

namespace Hospital.Application.Interfaces;

public interface IDoctorService
{
    Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request);
    Task<DoctorResponse> ObtenerPorIdAsync(Guid idDoctor);
    Task<IEnumerable<DoctorResponse>> ObtenerTodosAsync();
    Task ActualizarDoctorAsync(Guid idDoctor, UpdateDoctorRequest request);
    Task EliminarDoctorAsync(Guid idDoctor);
}
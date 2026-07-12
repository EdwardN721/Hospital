using Hospital.Application.DTOs.Requests.Specialties;
using Hospital.Application.DTOs.Responses.Specialties;

namespace Hospital.Application.Interfaces;

public interface ISpecialtyService
{
    Task<SpecialtyResponse> CreateSpecialtyAsync(CreateSpecialtyRequest request);
    Task<SpecialtyResponse> ObtenerPorIdAsync(Guid idSpecialty);
    Task<IEnumerable<SpecialtyResponse>> ObtenerTodosAsync();
    Task ActualizarSpecialtyAsync(Guid idSpecialty, UpdateSpecialtyRequest request);
    Task EliminarSpecialtyAsync(Guid idSpecialty);
}
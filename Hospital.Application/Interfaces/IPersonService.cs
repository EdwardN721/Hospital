using Hospital.Application.DTOs.Responses.Persons;

namespace Hospital.Application.Interfaces;

public interface IPersonService
{
    Task<PersonResponse> ObtenerPorIdAsync(Guid idPerson);
    Task<IEnumerable<PersonResponse>> ObtenerTodasAsync();
}
using Hospital.Application.DTOs.Responses.Persons;
using Hospital.Application.Interfaces;
using Hospital.Application.Mappers;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Core;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Services;

public class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IUnitOfWork unitOfWork, ILogger<PersonService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PersonResponse> ObtenerPorIdAsync(Guid idPerson)
    {
        _logger.LogInformation("Buscando a la persona con ID {Id}", idPerson);
        Person? person = await _unitOfWork.Personas.GetByIdAsync(idPerson);
        
        if (person == null) throw new NotFoundException(nameof(Person), idPerson);
        
        return person.MapToDto();
    }

    public async Task<IEnumerable<PersonResponse>> ObtenerTodasAsync()
    {
        IEnumerable<Person> persons = (await _unitOfWork.Personas.GetAllAsync()).ToList();

        _logger.LogInformation("Registros totales: {Count}", persons.Count());
        return persons.MapToDto();
    }
}
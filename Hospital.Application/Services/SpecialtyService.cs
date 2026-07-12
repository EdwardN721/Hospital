using FluentValidation;
using FluentValidation.Results;
using Hospital.Application.DTOs.Requests.Specialties;
using Hospital.Application.DTOs.Responses.Specialties;
using Hospital.Application.Interfaces;
using Hospital.Application.Mappers;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Core;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Services;

public class SpecialtyService : ISpecialtyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SpecialtyService> _logger;
    private readonly IValidator<CreateSpecialtyRequest> _createValidator;

    public SpecialtyService(IUnitOfWork unitOfWork, IValidator<CreateSpecialtyRequest> createValidator, ILogger<SpecialtyService> logger)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _logger = logger;
    }

    public async Task<SpecialtyResponse> CreateSpecialtyAsync(CreateSpecialtyRequest request)
    {
        _logger.LogInformation("Creando la especialidad {Name}", request.Name);

        ValidationResult? validationResult = await _createValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errorsDictionary = validationResult.Errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(g => g.Key, g => g.ToArray());

            throw new AppValidationException(errorsDictionary);
        }

        Specialty specialty = request.MapToEntity();

        await _unitOfWork.Especialidades.AddAsync(specialty);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Especialidad creada con éxito.");
        return specialty.MapToDto();
    }

    public async Task ActualizarSpecialtyAsync(Guid idSpecialty, UpdateSpecialtyRequest request)
    {
        _logger.LogInformation("Actualizando la especialidad {Id}", idSpecialty);
        Specialty specialty = await ObtenerEntidadPorId(idSpecialty);

        specialty.UpdateEntity(request);

        _unitOfWork.Especialidades.Update(specialty);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Especialidad actualizada.");
    }

    public async Task EliminarSpecialtyAsync(Guid idSpecialty)
    {
        _logger.LogWarning("Eliminando la especialidad {Id}", idSpecialty);
        Specialty specialty = await ObtenerEntidadPorId(idSpecialty);

        _unitOfWork.Especialidades.Delete(specialty);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Especialidad eliminada con éxito.");
    }

    public async Task<SpecialtyResponse> ObtenerPorIdAsync(Guid idSpecialty)
    {
        Specialty specialty = await ObtenerEntidadPorId(idSpecialty);
        return specialty.MapToDto();
    }

    public async Task<IEnumerable<SpecialtyResponse>> ObtenerTodosAsync()
    {
        var specialties = await _unitOfWork.Especialidades.GetAllAsync();
        return specialties.MapToDto();
    }

    #region Metodos Privados
    private async Task<Specialty> ObtenerEntidadPorId(Guid idSpecialty)
    {
        Specialty? specialty = await _unitOfWork.Especialidades.GetByIdAsync(idSpecialty);

        if (specialty == null)
        {
            throw new NotFoundException(nameof(Specialty), idSpecialty);
        }

        return specialty;
    }

    #endregion
}
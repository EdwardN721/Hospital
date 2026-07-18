using FluentValidation;
using FluentValidation.Results;
using Hospital.Application.DTOs.Requests.Doctors;
using Hospital.Application.DTOs.Responses.Doctors;
using Hospital.Application.Interfaces;
using Hospital.Application.Mappers;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Core;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DoctorService> _logger;
    private readonly IValidator<CreateDoctorRequest> _createValidator;

    public DoctorService(IUnitOfWork unitOfWork, IValidator<CreateDoctorRequest> createValidator, ILogger<DoctorService> logger)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _logger = logger;
    }

    public async Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request)
    {
        _logger.LogInformation("Creando al doctor {Nombre}", request.FirstName);
        ValidationResult? validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Error en datos ingresados.");
            var errors = validationResult.Errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                                                .ToDictionary(g => g.Key, g => g.ToArray());
            throw new AppValidationException(errors);
        }

        // Regla de Negocio: Validar que la especialidad exista antes de asignarla al doctor
        var specialty = await _unitOfWork.Especialidades.GetByIdAsync(request.SpecialtyId);
        if (specialty == null)
            throw new BusinessRuleException($"La especialidad con ID {request.SpecialtyId} no existe.");

        Doctor doctor = request.MapToEntity();
        await _unitOfWork.Doctores.AddAsync(doctor);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Doctor creado con éxito {Nombre}", request.FirstName);
        return doctor.MapToDto();
    }

    public async Task ActualizarDoctorAsync(Guid idDoctor, UpdateDoctorRequest request)
    {
        _logger.LogInformation("Actualizando información del doctor {Id}", idDoctor);
        Doctor doctor = await ObtenerEntidadPorId(idDoctor);
        
        // Si cambia de especialidad, validar que la nueva exista
        if (doctor.SpecialtyId != request.SpecialtyId)
        {
            _logger.LogError("Error en datos ingresados.");
            var specialty = await _unitOfWork.Especialidades.GetByIdAsync(request.SpecialtyId);
            if (specialty == null) throw new BusinessRuleException("La nueva especialidad no existe.");
        }

        doctor.UpdateEntity(request);
        _unitOfWork.Doctores.Update(doctor);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Información actualizada con éxito.");
    }

    public async Task EliminarDoctorAsync(Guid idDoctor)
    {
        _logger.LogWarning("Eliminando al doctor {Id}", idDoctor);
        Doctor doctor = await ObtenerEntidadPorId(idDoctor);
        _unitOfWork.Doctores.Delete(doctor);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogWarning("Doctor eliminado con éxito.");
    } 

    public async Task<DoctorResponse> ObtenerPorIdAsync(Guid idDoctor)
    {
        _logger.LogInformation("Obteniendo al doctor {Id}", idDoctor);
        Doctor doctor = await ObtenerEntidadPorId(idDoctor);
        return doctor.MapToDto();
    }

    public async Task<IEnumerable<DoctorResponse>> ObtenerTodosAsync()
    {
        _logger.LogInformation("Obteniendo doctores.");
        IEnumerable<Doctor> doctores = (await _unitOfWork.Doctores.GetAllAsync()).ToList();

        _logger.LogInformation("Doctores obtenidos {Count}", doctores.Count());
        return doctores.MapToDto();
    }

    #region Metodos Privados
    private async Task<Doctor> ObtenerEntidadPorId(Guid idDoctor)
    {
        Doctor? doctor = await _unitOfWork.Doctores.GetByIdAsync(idDoctor);
        if (doctor == null) throw new NotFoundException(nameof(Doctor), idDoctor);
        return doctor;
    }

    #endregion
}
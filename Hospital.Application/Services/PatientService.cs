using FluentValidation;
using FluentValidation.Results;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Models.Core;
using Hospital.Application.Mappers;
using Microsoft.Extensions.Logging;
using Hospital.Application.Interfaces;
using Hospital.Application.DTOs.Requests.Patients;
using Hospital.Application.DTOs.Responses.Patients;

namespace Hospital.Application.Services;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PatientService> _logger;

    private readonly IValidator<CreatePatientRequest> _createValidator;

    public PatientService(IUnitOfWork unitOfWork, IValidator<CreatePatientRequest> createValidator, ILogger<PatientService> logger)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _logger = logger;
    }

    public async Task ActualizarPacienteAsync(Guid idPaciente, UpdatePatientRequest request)
    {
        _logger.LogInformation("Actualizando la información del paciente {Id}", idPaciente);
        Patient paciente = await ObtenerPorId(idPaciente);

        paciente.UpdateEntity(request);

        _unitOfWork.Pacientes.Update(paciente);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Información actualizada.");
    }

    public async Task<PatientResponse> CreatePatientAsync(CreatePatientRequest request)
    {
        _logger.LogInformation("Creando la información del paciente {Nombre}", request.FirstName);

        // Ejecutar la validación
        ValidationResult? validationResult = await _createValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            // Agrupar los errores por nombre
            var errorsDictionary = validationResult.Errors
                .GroupBy(
                    e => e.PropertyName,
                    e => e.ErrorMessage)
                .ToDictionary(
                    failureGroup => failureGroup.Key,
                    failureGroup => failureGroup.ToArray());

            // Lanzar excepción personalizada
            throw new AppValidationException(errorsDictionary);
        }

        Patient patient = request.MapToEntity();

        await _unitOfWork.Pacientes.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Paciente creado con éxito.");
        return patient.MapToDto();
    }

    public async Task EliminarPacienteAsync(Guid idPaciente)
    {
        _logger.LogWarning("Eliminando al paciente {Id}", idPaciente);
        Patient paciente = await ObtenerPorId(idPaciente);

        _unitOfWork.Pacientes.Delete(paciente);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Paciente eliminado con éxito.");
    }

    public async Task<PatientResponse> ObtenerPorIdAsync(Guid idPaciente)
    {
        _logger.LogInformation("Obteniendo información del paciente {Id}", idPaciente);
        Patient paciente = await ObtenerPorId(idPaciente);

        return paciente.MapToDto();
    }

    public async Task<IEnumerable<PatientResponse>> ObtenerTodosAsync()
    {
        _logger.LogInformation("Obteniendo información de los pacientes");
        IEnumerable<Patient> pacientes = (await _unitOfWork.Pacientes.GetAllAsync()).ToList();

        _logger.LogInformation("Pacientes obtenidos {Count}", pacientes.Count());
        return pacientes.MapToDto();
    }

    #region Metodos Privados

    private async Task<Patient> ObtenerPorId(Guid idPaciente)
    {
        Patient? paciente = await _unitOfWork.Pacientes.GetByIdAsync(idPaciente); 

        if (paciente == null)
        {
            _logger.LogWarning("El paciente con el Id: {Id}, no se encontró.", idPaciente);
            throw new NotFoundException(nameof(Doctor), idPaciente);
        }

        return paciente;
    }

    #endregion
}
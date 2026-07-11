using FluentValidation;
using FluentValidation.Results;
using Hospital.Application.DTOs.Requests.Patients;
using Hospital.Application.DTOs.Responses.Patients;
using Hospital.Application.Interfaces;
using Hospital.Application.Mappers;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Core;

namespace Hospital.Application.Services;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePatientRequest> _createValidator;

    public PatientService(IUnitOfWork unitOfWork, IValidator<CreatePatientRequest> createValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
    }

    public async Task<PatientResponse> CreatePatientAsync(CreatePatientRequest request)
    {
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

        return patient.MapToDto();
    }
}
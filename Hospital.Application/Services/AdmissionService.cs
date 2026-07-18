using FluentValidation;
using Hospital.Application.DTOs.Requests.Admissions;
using Hospital.Application.DTOs.Responses.Admissions;
using Hospital.Application.Interfaces;
using Hospital.Application.Mappers;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Clinical;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Services;

public class AdmissionService : IAdmissionService 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateAdmissionRequest> _validator;
    private readonly ILogger<AdmissionService> _logger;

    public AdmissionService(IUnitOfWork unitOfWork, IValidator<CreateAdmissionRequest> validator, ILogger<AdmissionService> logger)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<AdmissionResponse> CreateAdmissionAsync(CreateAdmissionRequest request)
    {
        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsValid) throw new AppValidationException(validation.Errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage).ToDictionary(g => g.Key, g => g.ToArray()));

        // 1. Verificar existencia de la Cama
        var bed = await _unitOfWork.Camas.GetByIdAsync(request.BedId);
        if (bed == null) throw new BusinessRuleException("La cama especificada no existe.");

        // 2. REGLA CLÍNICA: La cama debe estar disponible
        if (bed.Status != "Available")
            throw new BusinessRuleException($"No se puede internar al paciente. La cama está actualmente: {bed.Status}");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // 3. Crear el internamiento
            Admission admission = request.MapToEntity();
            await _unitOfWork.Admisiones.AddAsync(admission);

            // 4. Cambiar el estado de la cama a 'Ocupada'
            bed.Status = "Occupied";
            _unitOfWork.Camas.Update(bed);

            // 5. Guardar todo
            await _unitOfWork.CommitTransactionAsync();
            return admission.MapToDto();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task ActualizarAdmissionAsync(Guid idAdmission, UpdateAdmissionRequest request)
    {
        var admission = await _unitOfWork.Admisiones.GetByIdAsync(idAdmission);
        if (admission == null) throw new NotFoundException(nameof(Admission), idAdmission);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            admission.Reason = request.Reason;

            if (request.DischargeDate.HasValue && !admission.DischargeDate.HasValue)
            {
                admission.DischargeDate = request.DischargeDate;
                
                var bed = await _unitOfWork.Camas.GetByIdAsync(admission.BedId);
                if (bed != null)
                {
                    bed.Status = "Available"; 
                    _unitOfWork.Camas.Update(bed);
                }
            }

            _unitOfWork.Admisiones.Update(admission);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    

    public async Task<AdmissionResponse> ObtenerAdmissionPorIdAsync(Guid idAdmission)
    {
        var admission = await _unitOfWork.Admisiones.GetByIdAsync(idAdmission);
        if (admission == null) throw new NotFoundException(nameof(Admission), idAdmission);

        return admission.MapToDto();
    }

    public async Task<IEnumerable<AdmissionResponse>> ObtenerTodasAdmissionsAsync()
    {
        var admissions = await _unitOfWork.Admisiones.GetAllAsync();
        return admissions.Select(a => a.MapToDto());
    }

    public async Task EliminarAdmissionAsync(Guid idAdmission)
    {
        var admission = await _unitOfWork.Admisiones.GetByIdAsync(idAdmission);
        if (admission == null) throw new NotFoundException(nameof(Admission), idAdmission);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var bed = await _unitOfWork.Camas.GetByIdAsync(admission.BedId);
            if (bed != null && admission.DischargeDate.HasValue)
            {
                bed.Status = "Available"; 
                _unitOfWork.Camas.Update(bed);
            }

            _unitOfWork.Admisiones.Delete(admission);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
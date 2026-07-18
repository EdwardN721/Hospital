using Hospital.Application.DTOs.Requests.Prescriptions;

namespace Hospital.Application.Interfaces;

public interface IPrescriptionService
{
    Task<Guid> CreatePrescriptionAsync(CreatePrescriptionRequest request);

}
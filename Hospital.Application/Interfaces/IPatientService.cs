using Hospital.Application.DTOs.Requests.Patients;
using Hospital.Application.DTOs.Responses.Patients;

namespace Hospital.Application.Interfaces;

public interface IPatientService
{
    Task<PatientResponse> CreatePatientAsync(CreatePatientRequest request);
} 
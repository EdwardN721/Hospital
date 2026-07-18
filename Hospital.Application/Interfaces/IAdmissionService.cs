using Hospital.Application.DTOs.Requests.Admissions;
using Hospital.Application.DTOs.Responses.Admissions;

namespace Hospital.Application.Interfaces;
public interface IAdmissionService
{
    Task<AdmissionResponse> CreateAdmissionAsync(CreateAdmissionRequest request);
    Task ActualizarAdmissionAsync(Guid idAdmission, UpdateAdmissionRequest request);
    Task<AdmissionResponse> ObtenerAdmissionPorIdAsync(Guid idAdmission);
    Task<IEnumerable<AdmissionResponse>> ObtenerTodasAdmissionsAsync();
    Task EliminarAdmissionAsync(Guid idAdmission);
}
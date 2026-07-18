using Hospital.Application.DTOs.Requests.Consultations;
using Hospital.Application.DTOs.Responses.Consultations;

namespace Hospital.Application.Interfaces;

public interface IConsultationService
{
    Task<ConsultationResponse> CreateConsultationAsync(CreateConsultationRequest request);
    Task<ConsultationResponse> ObtenerPorIdAsync(Guid idConsultation);
}
using Hospital.Application.DTOs.Requests.Consultations;
using Hospital.Application.DTOs.Responses.Consultations;
using Hospital.Domain.Models.Clinical;

namespace Hospital.Application.Mappers;

public static class ConsultationMapper
{
    public static ConsultationResponse MapToDto(this Consultation consultation)
    {
        return new ConsultationResponse(
            consultation.ConsultationId,
            consultation.AppointmentId,
            consultation.StartTime,
            consultation.EndTime,
            consultation.Notes
        );
    }

    public static Consultation MapToEntity(this CreateConsultationRequest request)
    {
        return new Consultation
        {
            AppointmentId = request.AppointmentId,
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            StartTime = request.StartTime,
            Notes = request.Notes,
            SystolicPressure = request.SystolicPressure,
            DiastolicPressure = request.DiastolicPressure,
            HeartRate = request.HeartRate,
            Temperature = request.Temperature
        };
    }
}
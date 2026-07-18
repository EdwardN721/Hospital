using Hospital.Application.DTOs.Requests.Admissions;
using Hospital.Application.DTOs.Responses.Admissions;
using Hospital.Domain.Models.Clinical;

namespace Hospital.Application.Mappers;

public static class AdmissionMapper
{
    public static AdmissionResponse MapToDto(this Admission admission)
    {
        return new AdmissionResponse(
            admission.AdmissionId,
            admission.PatientId,
            admission.BedId,
            admission.DoctorId,
            admission.AdmissionDate,
            admission.DischargeDate,
            admission.Reason
        );
    }

    public static Admission MapToEntity(this CreateAdmissionRequest request)
    {
        return new Admission
        {
            PatientId = request.PatientId,
            BedId = request.BedId,
            DoctorId = request.DoctorId,
            Reason = request.Reason,
            AdmissionDate = DateTimeOffset.UtcNow
        };
    }
}
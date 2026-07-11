using Hospital.Application.DTOs.Requests.Patients;
using Hospital.Application.DTOs.Responses.Patients;
using Hospital.Domain.Models.Core;

namespace Hospital.Application.Mappers;

public static class PatientsMapper
{
    public static PatientResponse MapToDto(this Patient patient)
    {
        return new PatientResponse(
            patient.PatientId,
            patient.FirstName,
            patient.LastName,
            patient.DateOfBirth,
            patient.Gender,
            patient.DocumentId,
            patient.Email,
            patient.BloodType,
            patient.EmergencyContactName,
            patient.EmergencyContactPhone
        );
    }

    public static Patient MapToEntity(this CreatePatientRequest request)
    {
        return new Patient
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            DocumentId = request.DocumentId,
            Email = request.Email,
            BloodType = request.BloodType,
            EmergencyContactName = request.EmergencyContactName,
            EmergencyContactPhone = request.EmergencyContactPhone
        };
    }

    public static IEnumerable<PatientResponse> MapToDto(this IEnumerable<Patient>? patients)
    {
        return patients?.Select(MapToDto) ?? Enumerable.Empty<PatientResponse>();
    }
}
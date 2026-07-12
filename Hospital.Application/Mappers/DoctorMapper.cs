using Hospital.Application.DTOs.Requests.Doctors;
using Hospital.Application.DTOs.Responses.Doctors;
using Hospital.Domain.Models.Core;

namespace Hospital.Application.Mappers;

public static class DoctorMapper
{
    public static DoctorResponse MapToDto(this Doctor doctor)
    {
        return new DoctorResponse(
            doctor.PersonId,
            doctor.FirstName,
            doctor.LastName,
            doctor.DateOfBirth,
            doctor.DocumentId,
            doctor.Email,
            doctor.SpecialtyId,
            doctor.MedicalLicense
        );
    }

    public static IEnumerable<DoctorResponse> MapToDto(this IEnumerable<Doctor>? doctors)
    {
        return doctors?.Select(d => d.MapToDto()) ?? Enumerable.Empty<DoctorResponse>();
    }

    public static Doctor MapToEntity(this CreateDoctorRequest request)
    {
        return new Doctor
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            DocumentId = request.DocumentId,
            Email = request.Email,
            SpecialtyId = request.SpecialtyId,
            MedicalLicense = request.MedicalLicense
        };
    }

    public static void UpdateEntity(this Doctor doctor, UpdateDoctorRequest request)
    {
        doctor.FirstName = request.FirstName;
        doctor.LastName = request.LastName;
        doctor.DateOfBirth = request.DateOfBirth;
        doctor.Gender = request.Gender;
        doctor.Email = request.Email;
        doctor.SpecialtyId = request.SpecialtyId;
        doctor.MedicalLicense = request.MedicalLicense;
    }
}
using Hospital.Application.DTOs.Requests.Specialties;
using Hospital.Application.DTOs.Responses.Specialties;
using Hospital.Domain.Models.Core;

namespace Hospital.Application.Mappers;

public static class SpecialtyMapper
{
    public static SpecialtyResponse MapToDto(this Specialty specialty)
    {
        return new SpecialtyResponse(
            specialty.SpecialtyId,
            specialty.Name,
            specialty.Description
        );
    }

    public static IEnumerable<SpecialtyResponse> MapToDto(this IEnumerable<Specialty>? specialties)
    {
        return specialties?.Select(s => s.MapToDto()) ?? Enumerable.Empty<SpecialtyResponse>();
    }

    public static Specialty MapToEntity(this CreateSpecialtyRequest request)
    {
        return new Specialty
        {
            Name = request.Name,
            Description = request.Description
        };
    }

    public static void UpdateEntity(this Specialty specialty, UpdateSpecialtyRequest request)
    {
        specialty.Name = request.Name;
        specialty.Description = request.Description;
    }
}
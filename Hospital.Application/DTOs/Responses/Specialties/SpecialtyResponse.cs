namespace Hospital.Application.DTOs.Responses.Specialties;

public record SpecialtyResponse(
    Guid SpecialtyId, 
    string Name, 
    string? Description
);
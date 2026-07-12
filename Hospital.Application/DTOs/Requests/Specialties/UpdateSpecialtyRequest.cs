namespace Hospital.Application.DTOs.Requests.Specialties;

public record UpdateSpecialtyRequest(
    string Name, 
    string? Description
);
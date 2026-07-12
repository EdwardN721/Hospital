namespace Hospital.Application.DTOs.Requests.Specialties;

public record CreateSpecialtyRequest(
    string Name, 
    string? Description
);
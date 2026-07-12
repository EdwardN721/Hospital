namespace Hospital.Application.DTOs.Responses.Persons;

public record PersonResponse(
    Guid PersonId,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    char? Gender,
    string DocumentId,
    string? Email
);
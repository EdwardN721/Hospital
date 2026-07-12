using Hospital.Application.DTOs.Responses.Persons;
using Hospital.Domain.Models.Core;

namespace Hospital.Application.Mappers;

public static class PersonMapper
{
    public static PersonResponse MapToDto(this Person person)
    {
        return new PersonResponse(
            person.PersonId,
            person.FirstName,
            person.LastName,
            person.DateOfBirth,
            person.Gender,
            person.DocumentId,
            person.Email
        );
    }

    public static IEnumerable<PersonResponse> MapToDto(this IEnumerable<Person> persons)
    {
        return persons.Select(p => p.MapToDto());
    }
}
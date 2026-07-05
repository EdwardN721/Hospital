namespace Hospital.Domain.Models.Core;

public class Person : BaseEntity
{
    public Guid PersonId { get; set; } 
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public char? Gender { get; set; }
    public string DocumentId { get; set; } = null!;
    public string? Email { get; set; }
}

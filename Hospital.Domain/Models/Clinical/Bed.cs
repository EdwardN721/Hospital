namespace Hospital.Domain.Models.Clinical;

public class Bed : BaseEntity
{
    public Guid BedId { get; set; }
    public Guid RoomId { get; set; }
    public string BedNumber { get; set; } = null!;
    public string Status { get; set; } = null!; // Available, Occupied, Maintenance

    public Room Room { get; set; } = null!;
    public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
}
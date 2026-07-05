namespace Hospital.Domain.Models.Clinical;

public class Room : BaseEntity
{
    public Guid RoomId { get; set; }
    public Guid FloorId { get; set; }
    public string RoomNumber { get; set; } = null!; // Ej: '101A'
    public short MaxBeds { get; set; }

    public Floor Floor { get; set; } = null!;
    public ICollection<Bed> Beds { get; set; } = new List<Bed>();
}
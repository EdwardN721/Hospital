namespace Hospital.Domain.Models.Clinical;

public class Floor : BaseEntity
{
    public Guid FloorId { get; set; }
    public string Name { get; set; } = null!; // Ej: 'Piso 1 - Cardiología'

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
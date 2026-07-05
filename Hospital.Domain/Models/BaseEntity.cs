namespace Hospital.Domain.Models;

public abstract class BaseEntity
{
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedDate { get; set; }
    public Guid? DeletedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid RowVersion { get; set; } = Guid.NewGuid();
}

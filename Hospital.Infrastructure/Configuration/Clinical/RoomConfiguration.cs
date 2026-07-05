using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms", "clinical", t => t.HasCheckConstraint("CK_Room_MaxBeds", "\"MaxBeds\" > 0"));

        builder.HasKey(r => r.RoomId);
        builder.Property(r => r.RoomId).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(r => r.RoomNumber).HasMaxLength(20).IsRequired();
        builder.HasIndex(r => r.RoomNumber).IsUnique();

        builder.HasOne(r => r.Floor).WithMany(f => f.Rooms).HasForeignKey(r => r.FloorId).OnDelete(DeleteBehavior.Restrict);
    }
}
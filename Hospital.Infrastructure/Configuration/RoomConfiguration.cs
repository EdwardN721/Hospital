using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Room", "clinical");

        builder.Property(p => p.RoomId).HasDefaultValueSql("gen_random_uuid()");
    }
}
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class FloorConfiguration : IEntityTypeConfiguration<Floor>
{
    public void Configure(EntityTypeBuilder<Floor> builder)
    {
        builder.ToTable("Floor", "clinical");

        builder.Property(p => p.FloorId).HasDefaultValueSql("gen_random_uuid()");
    }
}
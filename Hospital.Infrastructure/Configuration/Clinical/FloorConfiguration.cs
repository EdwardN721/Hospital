using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class FloorConfiguration : IEntityTypeConfiguration<Floor>
{
    public void Configure(EntityTypeBuilder<Floor> builder)
    {
        builder.ToTable("floors", "clinical");

        builder.HasKey(f => f.FloorId);
        builder.Property(f => f.FloorId).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(f => f.Name).HasMaxLength(50).IsRequired();
    }
}
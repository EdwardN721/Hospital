using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class BedConfiguration : IEntityTypeConfiguration<Bed>
{
    public void Configure(EntityTypeBuilder<Bed> builder)
    {
        builder.ToTable("Bed", "clinical");

        builder.Property(p => p.BedId).HasDefaultValueSql("gen_random_uuid()");
    }
}
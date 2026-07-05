using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescription", "clinical");

        builder.Property(p => p.PrescriptionId).HasDefaultValueSql("gen_random_uuid()");
    }
}
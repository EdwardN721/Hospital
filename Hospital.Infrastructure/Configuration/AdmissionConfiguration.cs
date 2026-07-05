using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class AdmissionConfiguration : IEntityTypeConfiguration<Admission>
{
    public void Configure(EntityTypeBuilder<Admission> builder)
    {
        builder.ToTable("Admission", "clinical");

        builder.Property(p => p.AdmissionId).HasDefaultValueSql("gen_random_uuid()");
    }
}
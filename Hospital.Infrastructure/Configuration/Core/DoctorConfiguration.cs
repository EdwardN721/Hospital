using Hospital.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("doctors", "core");

        builder.Property(p => p.DoctorId).HasDefaultValueSql("gen_random_uuid()");
        builder.HasKey(d => d.DoctorId);

        builder.Property(d => d.MedicalLicense).HasMaxLength(50).IsRequired();
        builder.HasIndex(d => d.MedicalLicense).IsUnique();
    }
}
using Hospital.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("patients", "core");

        builder.Property(p => p.PatientId).HasDefaultValueSql("gen_random_uuid()");
        builder.HasKey(p => p.PatientId);

        builder.Property(p => p.BloodType).HasMaxLength(5);
        builder.Property(p => p.EmergencyContactName).HasMaxLength(100);
        builder.Property(p => p.EmergencyContactPhone).HasMaxLength(20);
    }
}
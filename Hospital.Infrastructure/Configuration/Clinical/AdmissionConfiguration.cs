using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class AdmissionConfiguration : IEntityTypeConfiguration<Admission>
{
    public void Configure(EntityTypeBuilder<Admission> builder)
    {
        builder.ToTable("admissions", "clinical");

        builder.HasKey(a => a.AdmissionId);
        builder.Property(a => a.AdmissionId).HasDefaultValueSql("gen_random_uuid()");

        builder.HasOne(a => a.Patient).WithMany(p => p.Admissions).HasForeignKey(a => a.PatientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(a => a.Bed).WithMany(b => b.Admissions).HasForeignKey(a => a.BedId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(a => a.Doctor).WithMany().HasForeignKey(a => a.DoctorId).OnDelete(DeleteBehavior.Restrict);
    }
}
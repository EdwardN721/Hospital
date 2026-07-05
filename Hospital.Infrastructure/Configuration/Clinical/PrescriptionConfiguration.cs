using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("prescriptions", "clinical");

        builder.HasKey(p => p.PrescriptionId);
        builder.Property(p => p.PrescriptionId).HasDefaultValueSql("gen_random_uuid()");

        builder.HasOne(p => p.Consultation).WithMany(c => c.Prescriptions).HasForeignKey(p => p.ConsultationId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.Patient).WithMany().HasForeignKey(p => p.PatientId).OnDelete(DeleteBehavior.Restrict);
    }
}
using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
{
    public void Configure(EntityTypeBuilder<Consultation> builder)
    {
        builder.ToTable("consultations", "clinical", t =>
        {
            t.HasCheckConstraint("CK_Consultation_Systolic", "\"SystolicPressure\" BETWEEN 50 AND 250");
            t.HasCheckConstraint("CK_Consultation_Diastolic", "\"DiastolicPressure\" BETWEEN 30 AND 150");
            t.HasCheckConstraint("CK_Consultation_HeartRate", "\"HeartRate\" BETWEEN 30 AND 250");
            t.HasCheckConstraint("CK_Consultation_Temp", "\"Temperature\" BETWEEN 30.0 AND 45.0");
        });

        builder.Property(p => p.ConsultationId).HasDefaultValueSql("gen_random_uuid()");

        builder.HasKey(c => c.ConsultationId);
        builder.Property(c => c.ConsultationId).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(c => c.Temperature).HasColumnType("NUMERIC(4,2)");

        // 1 a 1 con Cita
        builder.HasOne(c => c.Appointment)
               .WithOne(a => a.Consultation)
               .HasForeignKey<Consultation>(c => c.AppointmentId);
               
        builder.HasOne(c => c.Patient).WithMany().HasForeignKey(c => c.PatientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Doctor).WithMany().HasForeignKey(c => c.DoctorId).OnDelete(DeleteBehavior.Restrict);
    }
}
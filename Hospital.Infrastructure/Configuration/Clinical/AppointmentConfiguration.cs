using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("appointments", "clinical");

        builder.Property(p => p.AppointmentId).HasDefaultValueSql("gen_random_uuid()");
        builder.HasKey(a => a.AppointmentId);

        // Relaciones
        builder.HasOne(a => a.Patient).WithMany(p => p.Appointments).HasForeignKey(a => a.PatientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(a => a.Doctor).WithMany(d => d.Appointments).HasForeignKey(a => a.DoctorId).OnDelete(DeleteBehavior.Restrict);

        // Unique Constraint: Evita doble reserva
        builder.HasIndex(a => new { a.DoctorId, a.ScheduledTime }).IsUnique();
    }
}
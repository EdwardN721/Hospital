using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class BedConfiguration : IEntityTypeConfiguration<Bed>
{
    public void Configure(EntityTypeBuilder<Bed> builder)
    {
        builder.ToTable("beds", "clinical");

        builder.Property(p => p.BedId).HasDefaultValueSql("gen_random_uuid()");

        // Mapeo de longitudes
        builder.Property(p => p.BedNumber).HasMaxLength(20).IsRequired();
        builder.Property(p => p.Status).HasMaxLength(20).IsRequired();

        // Relación con Room 
        builder.HasOne(b => b.Room)
               .WithMany(r => r.Beds)
               .HasForeignKey(b => b.RoomId)
               .OnDelete(DeleteBehavior.Restrict);

        // Unique Constraint compuesto
        builder.HasIndex(b => new { b.RoomId, b.BedNumber }).IsUnique();
    }


}
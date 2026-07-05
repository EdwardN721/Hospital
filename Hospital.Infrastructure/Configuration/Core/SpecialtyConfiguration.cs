using Hospital.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
{
    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder.ToTable("specialties", "core");

        builder.HasKey(s => s.SpecialtyId);
        builder.Property(s => s.SpecialtyId).HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
        builder.HasIndex(s => s.Name).IsUnique();

        builder.HasMany(s => s.Doctors)
               .WithOne(d => d.Specialty)
               .HasForeignKey(d => d.SpecialtyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
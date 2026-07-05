using Hospital.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons", "core");
        builder.Property(p => p.PersonId).HasDefaultValueSql("gen_random_uuid()");
        builder.HasKey(p => p.PersonId);
        
        // Herencia TPT
        builder.UseTptMappingStrategy(); 

        builder.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.DocumentId).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(150);

        builder.HasIndex(p => p.DocumentId).IsUnique();
        builder.HasIndex(p => p.Email).IsUnique();
    }
}
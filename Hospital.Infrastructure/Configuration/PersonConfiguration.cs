using Hospital.Domain.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person", "core");

        builder.Property(p => p.PersonId).HasDefaultValueSql("gen_random_uuid()");

        builder.UseTptMappingStrategy();
    }
}
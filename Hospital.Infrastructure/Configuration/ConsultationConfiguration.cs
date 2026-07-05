using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Clinical;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
{
    public void Configure(EntityTypeBuilder<Consultation> builder)
    {
        builder.ToTable("Consultation", "clinical");

        builder.Property(p => p.ConsultationId).HasDefaultValueSql("gen_random_uuid()");
    }
}
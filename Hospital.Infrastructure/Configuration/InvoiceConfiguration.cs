using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Billing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoice", "clinical");

        builder.Property(p => p.InvoiceId).HasDefaultValueSql("gen_random_uuid()");
    }
}
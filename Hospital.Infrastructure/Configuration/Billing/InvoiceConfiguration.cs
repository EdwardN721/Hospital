using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Models.Billing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configuration;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices", "clinical", t => 
        {
            t.HasCheckConstraint("CK_Invoice_TotalCost", "\"TotalCost\" >= 0");
            t.HasCheckConstraint("CK_Invoice_Discount", "\"Discount\" >= 0");
        });

        builder.HasKey(i => i.InvoiceId);
        builder.Property(i => i.InvoiceId).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(i => i.TotalCost).HasColumnType("NUMERIC(12,2)").IsRequired();
        builder.Property(i => i.Discount).HasColumnType("NUMERIC(12,2)").HasDefaultValue(0m);
        builder.Property(i => i.Status).HasMaxLength(20).HasDefaultValue("Pending");

        builder.HasOne(i => i.Patient).WithMany().HasForeignKey(i => i.PatientId).OnDelete(DeleteBehavior.Restrict);
    }
}
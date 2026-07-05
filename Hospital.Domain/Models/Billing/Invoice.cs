using Hospital.Domain.Models.Core;

namespace Hospital.Domain.Models.Billing;

public class Invoice : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public Guid PatientId { get; set; }
    public decimal TotalCost { get; set; }
    public decimal Discount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Paid, Cancelled

    public Patient Patient { get; set; } = null!;
}
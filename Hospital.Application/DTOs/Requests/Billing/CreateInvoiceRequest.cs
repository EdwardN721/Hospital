namespace Hospital.Application.DTOs.Requests.Billing;

public record CreateInvoiceRequest(
    Guid PatientId,
    decimal TotalCost,
    decimal Discount
);
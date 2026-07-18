namespace Hospital.Application.DTOs.Responses.Billing;

public record InvoiceResponse(
    Guid InvoiceId,
    Guid PatientId,
    decimal TotalCost,
    decimal Discount,
    string Status
);
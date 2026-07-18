using Hospital.Application.DTOs.Requests.Billing;
using Hospital.Application.DTOs.Responses.Billing;

namespace Hospital.Application.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request);
    Task ActualizarStatusAsync(Guid idInvoice, UpdateInvoiceStatusRequest request);
    Task<InvoiceResponse> ObtenerInvoicePorIdAsync(Guid idInvoice);
    Task EliminarInvoiceAsync(Guid idInvoice);
}
using FluentValidation;
using Hospital.Application.DTOs.Requests.Billing;
using Hospital.Application.DTOs.Responses.Billing;
using Hospital.Application.Interfaces;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Billing;

namespace Hospital.Application.Services;

public class InvoiceService : IInvoiceService 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateInvoiceRequest> _validator;

    public InvoiceService(IUnitOfWork unitOfWork, IValidator<CreateInvoiceRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request)
    {
        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsValid) throw new AppValidationException(validation.Errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage).ToDictionary(g => g.Key, g => g.ToArray()));

        var patient = await _unitOfWork.Pacientes.GetByIdAsync(request.PatientId);
        if (patient == null) throw new BusinessRuleException("El paciente para facturar no existe.");

        var invoice = new Invoice
        {
            PatientId = request.PatientId,
            TotalCost = request.TotalCost,
            Discount = request.Discount,
            Status = "Pending" // Valor por defecto
        };

        await _unitOfWork.Facturas.AddAsync(invoice);
        await _unitOfWork.SaveChangesAsync();

        return new InvoiceResponse(invoice.InvoiceId, invoice.PatientId, invoice.TotalCost, invoice.Discount, invoice.Status);
    }

    public async Task ActualizarStatusAsync(Guid idInvoice, UpdateInvoiceStatusRequest request)
    {
        var validStatuses = new[] { "Pending", "Paid", "Cancelled" };
        if (!validStatuses.Contains(request.Status)) throw new BusinessRuleException("Estado de factura inválido.");

        var invoice = await _unitOfWork.Facturas.GetByIdAsync(idInvoice);
        if (invoice == null) throw new NotFoundException(nameof(Invoice), idInvoice);

        // Regla: No puedes cancelar una factura ya pagada.
        if (invoice.Status == "Paid" && request.Status == "Cancelled")
            throw new BusinessRuleException("No se puede cancelar una factura que ya ha sido pagada.");

        invoice.Status = request.Status;
        
        _unitOfWork.Facturas.Update(invoice);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<InvoiceResponse> ObtenerInvoicePorIdAsync(Guid idInvoice)
    {
        var invoice = await _unitOfWork.Facturas.GetByIdAsync(idInvoice);
        if (invoice == null) throw new NotFoundException(nameof(Invoice), idInvoice);

        return new InvoiceResponse(invoice.InvoiceId, invoice.PatientId, invoice.TotalCost, invoice.Discount, invoice.Status);
    }

    public async Task EliminarInvoiceAsync(Guid idInvoice)
    {
        var invoice = await _unitOfWork.Facturas.GetByIdAsync(idInvoice);
        if (invoice == null) throw new NotFoundException(nameof(Invoice), idInvoice);

        // Regla: No puedes eliminar una factura que ya ha sido pagada.
        if (invoice.Status == "Paid")
            throw new BusinessRuleException("No se puede eliminar una factura que ya ha sido pagada.");

        _unitOfWork.Facturas.Delete(invoice);
        await _unitOfWork.SaveChangesAsync();
    }
}
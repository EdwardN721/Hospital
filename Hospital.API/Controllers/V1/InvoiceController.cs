using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Billing;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;
    public InvoiceController(IInvoiceService invoiceService) { _invoiceService = invoiceService; }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateInvoiceRequest request)
    {
        var response = await _invoiceService.CreateInvoiceAsync(request);
        return Ok(response); 
    }

    [HttpPatch("{idFactura:guid}/status")]
    public async Task<IActionResult> ActualizarEstado([FromRoute] Guid idFactura, [FromBody] UpdateInvoiceStatusRequest request)
    {
        await _invoiceService.ActualizarStatusAsync(idFactura, request);
        return NoContent();
    }

    [HttpGet("{idFactura:guid}")]
    public async Task<IActionResult> ObtenerPorId([FromRoute] Guid idFactura)
    {
        var response = await _invoiceService.ObtenerInvoicePorIdAsync(idFactura);
        return Ok(response);
    }

    [HttpDelete("{idFactura:guid}")]
    public async Task<IActionResult> Eliminar([FromRoute] Guid idFactura)
    {
        await _invoiceService.EliminarInvoiceAsync(idFactura);
        return NoContent();
    }
}
using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Prescriptions;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CreatePrescriptionRequest request)
    {
        var prescriptionId = await _prescriptionService.CreatePrescriptionAsync(request);
        
        // Retornamos el ID genérico ya que las recetas suelen imprimirse o consultarse desde la vista de la Consulta
        return StatusCode(StatusCodes.Status201Created, new { Id = prescriptionId });
    }
}
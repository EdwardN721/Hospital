using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Admissions;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AdmissionController : ControllerBase
{
    private readonly IAdmissionService _admissionService;
    public AdmissionController(IAdmissionService admissionService) { _admissionService = admissionService; }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateAdmissionRequest request)
    {
        var response = await _admissionService.CreateAdmissionAsync(request);
        return Ok(response); // Retorna HTTP 200/201 con los datos.
    }

    [HttpPut("{idAdmision:guid}")]
    public async Task<IActionResult> Actualizar([FromRoute] Guid idAdmision, [FromBody] UpdateAdmissionRequest request)
    {
        await _admissionService.ActualizarAdmissionAsync(idAdmision, request);
        return NoContent();
    }

    [HttpGet("{idAdmision:guid}")]
    public async Task<IActionResult> ObtenerPorAdmissionId([FromRoute] Guid idAdmision)
    {
        var response = await _admissionService.ObtenerAdmissionPorIdAsync(idAdmision);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodasAdmissions()
    {
        var response = await _admissionService.ObtenerTodasAdmissionsAsync();
        return Ok(response);
    }

    [HttpDelete("{idAdmision:guid}")]
    public async Task<IActionResult> Eliminar([FromRoute] Guid idAdmision)
    {
        await _admissionService.EliminarAdmissionAsync(idAdmision);
        return NoContent();
    }
}
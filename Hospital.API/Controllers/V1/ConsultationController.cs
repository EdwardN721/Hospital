using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Consultations;
using Hospital.Application.DTOs.Responses.Consultations;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ConsultationController : ControllerBase
{
    private readonly IConsultationService _consultationService;

    public ConsultationController(IConsultationService consultationService)
    {
        _consultationService = consultationService;
    }

    [HttpGet("{idConsulta:guid}")]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId([FromRoute] Guid idConsulta)
    {
        return Ok(await _consultationService.ObtenerPorIdAsync(idConsulta));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ConsultationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CreateConsultationRequest request)
    {
        var response = await _consultationService.CreateConsultationAsync(request);
        return CreatedAtAction(nameof(ObtenerPorId), new { idConsulta = response.ConsultationId }, response);
    }
}
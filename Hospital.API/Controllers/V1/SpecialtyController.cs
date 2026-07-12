using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Specialties;
using Hospital.Application.DTOs.Responses.Specialties;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

/// <summary>
/// Controlador que administra las especialidades médicas.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SpecialtyController : ControllerBase
{
    private readonly ISpecialtyService _specialtyService;

    public SpecialtyController(ISpecialtyService specialtyService)
    {
        _specialtyService = specialtyService;
    }

    /// <summary>
    /// Obtener todas las especialidades médicas.
    /// </summary>
    /// <returns>Listado de las especialidades médicas.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SpecialtyResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodas()
    {
        var specialties = await _specialtyService.ObtenerTodosAsync();
        return Ok(specialties);
    }

    /// <summary>
    /// Obtiene una especialidad médica.
    /// </summary>
    /// <param name="idEspecialidad">Id de la especialidad médica.</param>
    /// <returns>Obtiene una especialidad médica.</returns>
    [HttpGet("{idEspecialidad:guid}")]
    [ProducesResponseType(typeof(SpecialtyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId([FromRoute] Guid idEspecialidad)
    {
        var specialty = await _specialtyService.ObtenerPorIdAsync(idEspecialidad);
        return Ok(specialty);
    }

    /// <summary>
    /// Crear una especialidad médica.
    /// </summary>
    /// <param name="request">Información para crear una especialidad médica.</param>
    /// <returns>Especialidad medica agragada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SpecialtyResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CreateSpecialtyRequest request)
    {
        var response = await _specialtyService.CreateSpecialtyAsync(request);
        
        return CreatedAtAction(nameof(ObtenerPorId), new { idEspecialidad = response.SpecialtyId }, response);
    }

    /// <summary>
    /// Actualiza una especialidad médica.
    /// </summary>
    /// <param name="idEspecialidad">Id de la especialidad médica.</param>
    /// <param name="request">Información a actualizar.</param>
    /// <returns>Estado de la actualización.</returns>
    [HttpPut("{idEspecialidad:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar([FromRoute] Guid idEspecialidad, [FromBody] UpdateSpecialtyRequest request)
    {
        await _specialtyService.ActualizarSpecialtyAsync(idEspecialidad, request);
        return NoContent();
    }

    /// <summary>
    /// Eliminar una especialidad médica.
    /// </summary>
    /// <param name="idEspecialidad">Id de la especialidad médica.</param>
    /// <returns>Estado de la eliminación.</returns>
    [HttpDelete("{idEspecialidad:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar([FromRoute] Guid idEspecialidad)
    {
        await _specialtyService.EliminarSpecialtyAsync(idEspecialidad);
        return NoContent();
    }
}
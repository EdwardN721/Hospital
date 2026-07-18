using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Doctors;
using Hospital.Application.DTOs.Responses.Doctors;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

/// <summary>
/// Controlador que administra a los doctores.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    /// <summary>
    /// Obtener a todos los doctores
    /// </summary>
    /// <returns>Listado de doctores.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DoctorResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodosDoctores()
    {
        return Ok(await _doctorService.ObtenerTodosAsync());
    }

    /// <summary>
    /// Obtener doctor por su Id.
    /// </summary>
    /// <param name="idDoctor">Id del doctor.</param>
    /// <returns>Doctor.</returns>
    [HttpGet("{idDoctor:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerDoctorPorId([FromRoute] Guid idDoctor)
    {
        return Ok(await _doctorService.ObtenerPorIdAsync(idDoctor));
    }

    /// <summary>
    /// Creando a un doctor.
    /// </summary>
    /// <param name="request">Información para crear un doctor.</param>
    /// <returns>Doctor creado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CrearDoctor([FromBody] CreateDoctorRequest request)
    {
        DoctorResponse response = await _doctorService.CreateDoctorAsync(request);
        return CreatedAtAction(nameof(ObtenerDoctorPorId), new { idDoctor = response.PersonId }, response);
    }

    /// <summary>
    /// Actualizar información de un doctor.
    /// </summary>
    /// <param name="idDoctor">Id del doctor para actualizar.</param>
    /// <param name="request">Información del doctor para actualizar.</param>
    /// <returns>Estado de la actualización.</returns>
    [HttpPut("{idDoctor:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar([FromRoute] Guid idDoctor, [FromBody] UpdateDoctorRequest request)
    {
        await _doctorService.ActualizarDoctorAsync(idDoctor, request);
        return NoContent();
    }

    /// <summary>
    /// Eliminar a un doctor.
    /// </summary>
    /// <param name="idDoctor">Id del doctor a eliminar.</param>
    /// <returns>Estado de la eliminación.</returns>
    [HttpDelete("{idDoctor:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar([FromRoute] Guid idDoctor)
    {
        await _doctorService.EliminarDoctorAsync(idDoctor);
        return NoContent();
    }
}
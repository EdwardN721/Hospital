using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Appointments;
using Hospital.Application.DTOs.Responses.Appointments;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

/// <summary>
/// Controlador que administra las citas.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    /// <summary>
    /// Obtener todas las citas.
    /// </summary>
    /// <returns>Listado de citas.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppointmentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodasCitas()
    {
        return Ok(await _appointmentService.ObtenerTodasAsync());
    }

    /// <summary>
    /// Obtener Cita por Id.
    /// </summary>
    /// <param name="idCita">Id de la cita.</param>
    /// <returns>Cita.</returns>
    [HttpGet("{idCita:guid}")]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerCitaPorId([FromRoute] Guid idCita)
    {
        return Ok(await _appointmentService.ObtenerPorIdAsync(idCita));
    }

    /// <summary>
    /// Crear una cita.
    /// </summary>
    /// <param name="request">Información para crear una cita.</param>
    /// <returns>Cita creada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CrearCita([FromBody] CreateAppointmentRequest request)
    {
        var response = await _appointmentService.CreateAppointmentAsync(request);
        return CreatedAtAction(nameof(ObtenerCitaPorId), new { idCita = response.AppointmentId }, response);
    }

    /// <summary>
    /// Actualizar cita.
    /// </summary>
    /// <param name="idCita">Id de la cita.</param>
    /// <param name="request">Información para actualizar cita.</param>
    /// <returns>Estado de la cita.</returns>
    [HttpPut("{idCita:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActualizarCita([FromRoute] Guid idCita, [FromBody] UpdateAppointmentRequest request)
    {
        await _appointmentService.ActualizarAppointmentAsync(idCita, request);
        return NoContent();
    }

    /// <summary>
    /// Eliminar cita.
    /// </summary>
    /// <param name="idCita">Id de la cita a eliminar.</param>
    /// <returns>Estado de la eliminación.</returns>
    [HttpDelete("{idCita:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar([FromRoute] Guid idCita)
    {
        await _appointmentService.EliminarAppointmentAsync(idCita);
        return NoContent();
    }
}
using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Patients;
using Hospital.Application.DTOs.Responses.Patients;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

/// <summary>
/// Controlador que administra a los pacientes.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    /// <summary>
    /// Obtiene todos los pacientes.
    /// </summary>
    /// <returns>Listado de pacientes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodosPacientes()
    {
        IEnumerable<PatientResponse> pacientes = await _patientService.ObtenerTodosAsync();
        return Ok(pacientes);
    }

    /// <summary>
    /// Obtiene a un paciente.
    /// </summary>
    /// <param name="idPaciente">Id del paciente.</param>
    /// <returns>Un paciente.</returns>
    [HttpGet("{idPaciente:guid}")]
    [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPaciente([FromRoute] Guid idPaciente)
    {
        PatientResponse paciente = await _patientService.ObtenerPorIdAsync(idPaciente);
        return Ok(paciente);
    }


    /// <summary>
    /// Crear a un paciente.
    /// </summary>
    /// <param name="request">Información para crear un paciente.</param>
    /// <returns>Paciente creado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CrearPaciente([FromBody] CreatePatientRequest request)
    {
        PatientResponse response = await _patientService.CreatePatientAsync(request);
        
        return CreatedAtAction(nameof(ObtenerPaciente), new { idPaciente = response.PatientId }, response);
    }

    /// <summary>
    /// Actualizar un paciente.
    /// </summary>
    /// <param name="idPaciente">Id del paciente a acutalizar.</param>
    /// <param name="request">Información del paciente a actualizar.</param>
    /// <returns>Estado de la actualización.</returns>
    [HttpPut("{idPaciente:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActualizarPaciente([FromRoute] Guid idPaciente, [FromBody] UpdatePatientRequest request)
    {
        await _patientService.ActualizarPacienteAsync(idPaciente, request);
        return NoContent();
    }

    /// <summary>
    /// Eliminar un paciente.
    /// </summary>
    /// <param name="idPaciente">Id del paciente a eliminar.</param>
    /// <returns>Estado de la eliminación.</returns>
    [HttpDelete("{idPaciente:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EliminarPaciente([FromRoute] Guid idPaciente)
    {
        await _patientService.EliminarPacienteAsync(idPaciente);
        return NoContent();
    }
}
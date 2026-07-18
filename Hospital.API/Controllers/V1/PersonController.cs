using Asp.Versioning;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

/// <summary>
/// Controlador que administra a las personas
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Obtener todas las personas.
    /// </summary>
    /// <returns>Listado de personas.</returns>
    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        return Ok(await _personService.ObtenerTodasAsync());
    }

    /// <summary>
    /// Obtener a una persona por su Id.
    /// </summary>
    /// <param name="idPersona">Id de la persona.</param>
    /// <returns>Una persona.</returns>
    [HttpGet("{idPersona:guid}")]
    public async Task<IActionResult> ObtenerPorId([FromRoute] Guid idPersona)
    {
        return Ok(await _personService.ObtenerPorIdAsync(idPersona));
    }
}
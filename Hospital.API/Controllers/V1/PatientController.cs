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

    [HttpPost]
    public async Task<IActionResult> Create(CreatePatientRequest request)
    {
        PatientResponse response = await _patientService.CreatePatientAsync(request);
        
        // return CreatedAtAction(nameof(GetById), new { id = response.PatientId }, response);
        return Ok("Creado.");
    }
}
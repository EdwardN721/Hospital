using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Beds;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BedController : ControllerBase
{
    private readonly IBedService _bedService;
    public BedController(IBedService bedService) { _bedService = bedService; }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateBedRequest request)
    {
        var response = await _bedService.CreateBedAsync(request);
        return Ok(response);
    }

    [HttpGet("{bedId}")]
    public async Task<IActionResult> ObtenerPorId(Guid bedId)
    {
        var response = await _bedService.GetBedByIdAsync(bedId);
        return Ok(response);
    }

    [HttpGet()]
    public async Task<IActionResult> ObtenerPorHabitacion(Guid roomId)
    {
        var response = await _bedService.GetBedsByRoomIdAsync(roomId);
        return Ok(response);
    }
}
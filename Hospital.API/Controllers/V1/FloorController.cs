using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Floors;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FloorController : ControllerBase
{
    private readonly IFloorService _floorService;
    public FloorController(IFloorService floorService) { _floorService = floorService; }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateFloorRequest request)
    {
        var response = await _floorService.CreateFloorAsync(request);
        return Ok(response); 
    }

    [HttpGet("{floorId}")]
    public async Task<IActionResult> ObtenerPorId(Guid floorId)
    {
        var response = await _floorService.GetFloorByIdAsync(floorId);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var response = await _floorService.GetAllFloorsAsync();
        return Ok(response);
    }
}
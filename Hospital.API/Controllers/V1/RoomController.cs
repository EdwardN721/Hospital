using Asp.Versioning;
using Hospital.Application.DTOs.Requests.Rooms;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    public RoomController(IRoomService roomService) { _roomService = roomService; }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateRoomRequest request)
    {
        var response = await _roomService.CreateRoomAsync(request);
        return Ok(response);
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> ObtenerPorId(Guid roomId)
    {
        var response = await _roomService.GetRoomByIdAsync(roomId);
        return Ok(response);
    }

    [HttpGet()]
    public async Task<IActionResult> ObtenerPorPiso(Guid floorId)
    {
        var response = await _roomService.GetRoomsByFloorIdAsync(floorId);
        return Ok(response);
    }
}
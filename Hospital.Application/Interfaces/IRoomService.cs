using Hospital.Application.DTOs.Requests.Rooms;
using Hospital.Application.DTOs.Responses.Rooms;

namespace Hospital.Application.Interfaces;

public interface IRoomService
{
    Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request);
    Task<RoomResponse> GetRoomByIdAsync(Guid roomId);
    Task<IEnumerable<RoomResponse>> GetRoomsByFloorIdAsync(Guid floorId);
}
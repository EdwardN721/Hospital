using Hospital.Application.DTOs.Requests.Rooms;
using Hospital.Application.DTOs.Responses.Rooms;
using Hospital.Application.Interfaces;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Clinical;

namespace Hospital.Application.Services;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

    public async Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request)
    {
        // Regla de Integridad
        var floor = await _unitOfWork.Pisos.GetByIdAsync(request.FloorId);
        if (floor == null) throw new BusinessRuleException("El piso especificado no existe.");

        var room = new Room 
        { 
            FloorId = request.FloorId, 
            RoomNumber = request.RoomNumber, 
            MaxBeds = request.MaxBeds 
        };

        await _unitOfWork.Habitaciones.AddAsync(room);
        await _unitOfWork.SaveChangesAsync();
        
        return new RoomResponse(room.RoomId, room.FloorId, room.RoomNumber, room.MaxBeds);
    }

    public async Task<RoomResponse> GetRoomByIdAsync(Guid roomId)
    {
        var room = await _unitOfWork.Habitaciones.GetByIdAsync(roomId);
        if (room == null) throw new BusinessRuleException("El cuarto especificado no existe.");

        return new RoomResponse(room.RoomId, room.FloorId, room.RoomNumber, room.MaxBeds);
    }

    public async Task<IEnumerable<RoomResponse>> GetRoomsByFloorIdAsync(Guid floorId)
    {
        var rooms = await _unitOfWork.Habitaciones.GetAllAsync();
        var filteredRooms = rooms.Where(r => r.FloorId == floorId);

        return filteredRooms.Select(r => new RoomResponse(r.RoomId, r.FloorId, r.RoomNumber, r.MaxBeds));
    }
}
namespace Hospital.Application.DTOs.Requests.Rooms;

public record CreateRoomRequest(
    Guid FloorId, 
    string RoomNumber, 
    short MaxBeds);
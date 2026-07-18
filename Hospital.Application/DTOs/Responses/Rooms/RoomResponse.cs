namespace Hospital.Application.DTOs.Responses.Rooms;

public record RoomResponse(
    Guid RoomId, 
    Guid FloorId, 
    string RoomNumber, 
    short MaxBeds);
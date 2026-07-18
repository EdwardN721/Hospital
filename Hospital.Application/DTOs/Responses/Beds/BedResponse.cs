namespace Hospital.Application.DTOs.Responses.Beds;

public record BedResponse(
    Guid BedId, 
    Guid RoomId, 
    string BedNumber, 
    string Status
    );
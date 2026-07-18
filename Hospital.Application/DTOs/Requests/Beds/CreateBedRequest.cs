namespace Hospital.Application.DTOs.Requests.Beds;

public record CreateBedRequest(
    Guid RoomId, 
    string BedNumber, 
    string Status
    );

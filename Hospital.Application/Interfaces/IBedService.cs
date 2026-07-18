using Hospital.Application.DTOs.Requests.Beds;
using Hospital.Application.DTOs.Responses.Beds;

namespace Hospital.Application.Interfaces;

public interface IBedService
{
    Task<BedResponse> CreateBedAsync(CreateBedRequest request);
    Task<BedResponse> GetBedByIdAsync(Guid bedId);
    Task<IEnumerable<BedResponse>> GetBedsByRoomIdAsync(Guid roomId);
}
using Hospital.Application.DTOs.Requests.Floors;
using Hospital.Application.DTOs.Responses.Floors;

namespace Hospital.Application.Interfaces;

public interface IFloorService
{
    Task<FloorResponse> CreateFloorAsync(CreateFloorRequest request);
    Task<FloorResponse> GetFloorByIdAsync(Guid floorId);
    Task<IEnumerable<FloorResponse>> GetAllFloorsAsync();
}
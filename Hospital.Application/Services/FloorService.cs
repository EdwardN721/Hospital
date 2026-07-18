using Hospital.Application.DTOs.Requests.Floors;
using Hospital.Application.DTOs.Responses.Floors;
using Hospital.Application.Interfaces;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Clinical;

namespace Hospital.Application.Services;

public class FloorService : IFloorService
{
    private readonly IUnitOfWork _unitOfWork;

    public FloorService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

    public async Task<FloorResponse> CreateFloorAsync(CreateFloorRequest request)
    {
        var floor = new Floor { Name = request.Name };
        await _unitOfWork.Pisos.AddAsync(floor);
        await _unitOfWork.SaveChangesAsync();
        return new FloorResponse(floor.FloorId, floor.Name);
    }
    
    public async Task<FloorResponse> GetFloorByIdAsync(Guid floorId)
    {
        var floor = await _unitOfWork.Pisos.GetByIdAsync(floorId);
        if (floor == null) throw new Exception("El piso especificado no existe.");
        return new FloorResponse(floor.FloorId, floor.Name);
    }

    public async Task<IEnumerable<FloorResponse>> GetAllFloorsAsync()
    {
        var floors = await _unitOfWork.Pisos.GetAllAsync();
        return floors.Select(f => new FloorResponse(f.FloorId, f.Name));
    }
}
using Hospital.Application.DTOs.Requests.Beds;
using Hospital.Application.DTOs.Responses.Beds;
using Hospital.Application.Interfaces;
using Hospital.Domain.Exceptions;
using Hospital.Domain.Interfaces;
using Hospital.Domain.Models.Clinical;

namespace Hospital.Application.Services;

public class BedService : IBedService
{
    private readonly IUnitOfWork _unitOfWork;

    public BedService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

    public async Task<BedResponse> GetBedByIdAsync(Guid bedId)
    {
        var bed = await _unitOfWork.Camas.GetByIdAsync(bedId);
        if (bed == null) throw new BusinessRuleException("La cama especificada no existe.");

        return new BedResponse(bed.BedId, bed.RoomId, bed.BedNumber, bed.Status);
    }

    public async Task<IEnumerable<BedResponse>> GetBedsByRoomIdAsync(Guid roomId)
    {
        var beds = await _unitOfWork.Camas.GetAllAsync();
        var filteredBeds = beds.Where(b => b.RoomId == roomId);

        return filteredBeds.Select(b => new BedResponse(b.BedId, b.RoomId, b.BedNumber, b.Status));
    }

    public async Task<BedResponse> CreateBedAsync(CreateBedRequest request)
    {
        // 1. Validar que el cuarto exista
        var room = await _unitOfWork.Habitaciones.GetByIdAsync(request.RoomId);
        if (room == null) throw new BusinessRuleException("El cuarto especificado no existe.");

        // 2. Regla de Negocio: Validar capacidad
        // Idealmente, agregarías un método en tu Repositorio: await _unitOfWork.Camas.CountByRoomIdAsync(request.RoomId);
        var existingBeds = (await _unitOfWork.Camas.GetAllAsync())
                            .Where(b => b.RoomId == request.RoomId)
                            .ToList();

        if (existingBeds.Count >= room.MaxBeds)
        {
            throw new BusinessRuleException($"El cuarto {room.RoomNumber} ya ha alcanzado su capacidad máxima de {room.MaxBeds} camas.");
        }

        // 3. Validar estado permitido
        var validStatuses = new[] { "Available", "Occupied", "Maintenance" };
        if (!validStatuses.Contains(request.Status))
        {
            throw new BusinessRuleException("Estado de cama inválido.");
        }

        var bed = new Bed 
        { 
            RoomId = request.RoomId, 
            BedNumber = request.BedNumber, 
            Status = request.Status 
        };

        await _unitOfWork.Camas.AddAsync(bed);
        await _unitOfWork.SaveChangesAsync();
        
        return new BedResponse(bed.BedId, bed.RoomId, bed.BedNumber, bed.Status);
    }
}
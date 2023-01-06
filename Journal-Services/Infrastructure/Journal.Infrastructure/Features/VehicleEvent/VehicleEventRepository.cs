using Journal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Features.VehicleEvent;

/// <inheritdoc/>
public class VehicleEventRepository : IVehicleEventRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleEventRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc/>
    public int InsertVehicleEvent(VehicleEventDTO dto)
    {
        _dbContext.VehicleEvent.Add(dto);
        _dbContext.SaveChanges();

        return dto.VehicleEventId;
    }

    /// <inheritdoc/>
    public IList<VehicleEventDTO> GetVehicleEventsByVehicleId(int vehicleId)
    {
        var events =
            _dbContext.VehicleEvent
                .Where(vehicleEvent => vehicleEvent.VehicleId == vehicleId)
                .OrderByDescending(vehicleEvent => vehicleEvent.EventDate)
                .ThenByDescending(vehicleEvent => vehicleEvent.VehicleOdometer)
                .AsNoTracking()
                .ToList();

        return events;
    }

    /// <inheritdoc/>
    public IList<VehicleEventDTO> GetVehicleEventsByVehicleIdWithDate(int vehicleId, DateTimeOffset dateLimit){
        var events =
            _dbContext.VehicleEvent
                .Where(vehicleEvent => vehicleEvent.VehicleId == vehicleId &&
                                       vehicleEvent.EventDate <= dateLimit)
                .OrderByDescending(vehicleEvent => vehicleEvent.EventDate)
                .ThenByDescending(vehicleEvent => vehicleEvent.VehicleOdometer)
                .AsNoTracking()
                .ToList();

        return events;
    }
}
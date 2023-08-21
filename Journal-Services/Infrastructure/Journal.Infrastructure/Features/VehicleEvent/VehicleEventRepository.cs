using Dapper;
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

    public IList<VehicleEventDTO> GetVehicleEventsByVehicleSecondaryId(Guid vehicleId)
    {
        const string SQL = @"
            select
                e.vehicle_event_id as VehicleEventId,
                e.owner_driver_id as OwnerDriverId,
                e.vehicle_id as VehicleId,
                e.driver_id as DriverId,
                e.event_date as EventDate,
                e.vehicle_odometer as VehicleOdometer,
                e.vehicle_event_type_id as VehicleEventTypeId,
                e.event_description as EvendDescription,
                e.event_note as EventNote
            from
                vehicle_event e
                inner join vehicle v on e.vehicle_id = v.vehicle_id 
            where 
                v.secondary_id = @SecondaryId";

        using (var con = _dbContext.GetConnection())
        {
            var vehicles = con.Query<VehicleEventDTO>(SQL, new { SecondaryId = vehicleId });

            return vehicles.ToList();
        }
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
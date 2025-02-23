using Dapper;
using Journal.Domain.Base;
using Journal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Features.VehicleEvent;

/// <inheritdoc/>
public class VehicleEventRepository : IVehicleEventRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleEventRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc/>
    public ModelDoublePK InsertVehicleEvent(VehicleEventDTO dto)
    {
        _dbContext.VehicleEvent.Add(dto);
        _dbContext.SaveChanges();

        return new ModelDoublePK(dto.VehicleEventId, dto.SecondaryId);
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
                e.secondary_id as SecondaryId,
                e.owner_driver_id as OwnerDriverId,
                owner_driver.secondary_id as OwnerDriverSecondaryId,
                e.vehicle_id as VehicleId,
                v.secondary_id as VehicleSecondaryId,
                e.driver_id as DriverId,
                d.secondary_id as DriverSecondaryId,
                e.event_date as EventDate,
                e.vehicle_odometer as VehicleOdometer,
                e.vehicle_event_type_id as VehicleEventTypeId,
                e.event_description as EventDescription,
                e.event_note as EventNote
            from
                vehicle_event e
                inner join vehicle v on e.vehicle_id = v.vehicle_id 
                inner join driver owner_driver on e.owner_driver_id = owner_driver.driver_id
                inner join driver d on e.driver_id = d.driver_id
            where 
                v.secondary_id = @SecondaryId";

        using (var con = _dbContext.GetConnection())
        {
            var vehicles = con.Query<VehicleEventDTO>(SQL, new { SecondaryId = vehicleId });

            return vehicles.ToList();
        }
    }

    /// <inheritdoc/>
    public IList<VehicleEventDTO> GetVehicleEventsByVehicleIdWithDate(long vehicleId, DateTimeOffset dateLimit){
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
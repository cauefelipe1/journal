using Dapper;
using Journal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Features.Vehicle;

/// <inheritdoc/>
public class VehicleRepository : IVehicleRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc/>
    public long InsertVehicle(VehicleDTO dto)
    {
        _dbContext.Vehicle.Add(dto);
        _dbContext.SaveChanges();

        return dto.VehicleId;
    }

    /// <inheritdoc/>
    public VehicleDTO? GetVehicleById(long id)
    {
        var vehicle =
            _dbContext.Vehicle.AsNoTracking()
                .SingleOrDefault(v => v.VehicleId == id);

        return vehicle;
    }

    /// <inheritdoc/>
    public VehicleDTO? GetVehicleBySecondaryId(Guid secondaryId)
    {
        var vehicle =
            _dbContext.Vehicle.AsNoTracking()
                .SingleOrDefault(v => v.SecondaryId == secondaryId);

        return vehicle;
    }

    private const string GET_VEHICLE_SQL = @"
            select 
                v.vehicle_id as VehicleId,
                v.secondary_id as SecondaryId,
                v.model as Model, 
                v.nickname as Nickname, 
                v.vehicle_type_id as VehicleTypeId, 
                v.vehicle_brand_id as VehicleBrandId, 
                vb.secondary_id as VehicleBrandSecondaryId,
                v.model_year as ModelYear, 
                v.main_driver_id as MainDriverId,
                d.secondary_id as MainDriverSecondaryId
            from
                vehicle v
                inner join driver d on v.main_driver_id = d.driver_id
                inner join vehicle_brand vb on v.vehicle_brand_id = vb.vehicle_brand_id";

    /// <inheritdoc/>
    public IList<VehicleDTO> GetVehicleByMainDriverId(int mainDriverId)
    {
        const string SQL = GET_VEHICLE_SQL + @"
            where
                d.main_driver_id = @MainDriverId";

        using (var con = _dbContext.GetConnection())
        {
            var vehicles = con.Query<VehicleDTO>(SQL, new { MainDriverId = mainDriverId });

            return vehicles.ToList();
        }
    }

    /// <inheritdoc/>
    public IList<VehicleDTO> GetVehicleByMainDriverSecondaryId(Guid mainDriverId)
    {
        const string SQL = GET_VEHICLE_SQL + @"
            where
                d.secondary_id = @SecondaryId";

        using (var con = _dbContext.GetConnection())
        {
            var vehicles = con.Query<VehicleDTO>(SQL, new { SecondaryId = mainDriverId });

            return vehicles.ToList();
        }
    }
}
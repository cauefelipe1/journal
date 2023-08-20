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

    public VehicleDTO? GetVehicleById(int id)
    {
        var vehicle =
            _dbContext.Vehicle.AsNoTracking()
                .FirstOrDefault(v => v.VehicleId == id);

        return vehicle;
    }

    /// <inheritdoc/>
    public IList<VehicleDTO> GetVehicleByMainDriverId(int mainDriverId)
    {
        var vehicles =
            _dbContext.Vehicle.AsNoTracking()
                .Where(vehicle => vehicle.MainDriverId == mainDriverId)
                .ToList();

        return vehicles;
    }

    /// <inheritdoc/>
    public IList<VehicleDTO> GetVehicleByMainDriverSecondaryId(Guid mainDriverId)
    {
        const string SQL =
            @"
            select 
                v.vehicle_id as VehicleId,
                v.secondary_id as SecondaryId,
                v.model as Model, 
                v.nickname as Nickname, 
                v.vehicle_type_id as VehicleTypeId, 
                v.vehicle_brand_id as VehicleBrandId, 
                v.model_year as ModelYear, 
                v.main_driver_id as MainDriverId
            from
                vehicle v
                inner join driver d on v.main_driver_id = d.driver_id
            where
                d.secondary_id = @SecondaryId";

        using (var con = _dbContext.GetConnection())
        {
            var vehicles = con.Query<VehicleDTO>(SQL, new { SecondaryId = mainDriverId });

            return vehicles.ToList();
        }
    }

    /// <inheritdoc/>
    public IList<VehicleBrandDTO> GetAllBrands()
    {
        var brands =
            _dbContext.VehicleBrand
                .Where(brand => brand.VehicleBrandId > 0)
                .AsNoTracking()
                .ToList();

        return brands;
    }
}
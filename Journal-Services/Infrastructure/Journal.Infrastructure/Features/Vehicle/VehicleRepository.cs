using Dapper;
using Journal.Domain.Models.Vehicle;
using Journal.Infrastructure.Database;
using Journal.SharedSettings;

namespace Journal.Infrastructure.Features.Vehicle;

public class VehicleRepository : IVehicleRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    private const string GET_ALL_BRANDS_SQL = @"
        SELECT 
            vehicle_brand_id AS Id,
            vehicle_brand_name AS Name,
            country_id AS CountryId
        FROM
            vehicle_brand
        WHERE
            vehicle_brand_id > 0";

    public IList<VehicleBrand> GetAllBrands()
    {
        using var conn = _dbContext.GetConnection();

        var brands = conn.Query<VehicleBrand>(GET_ALL_BRANDS_SQL).ToList();

        return brands;
    }
}
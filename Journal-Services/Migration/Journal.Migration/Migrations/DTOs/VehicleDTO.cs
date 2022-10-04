// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Journal.Migration.Migrations.DTOs;

public class VehicleBrandDTO
{
    public int vehicle_brand_id { get; set; }

    public string? name { get; set; }

    public int country_id { get; set; }
}
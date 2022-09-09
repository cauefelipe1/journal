// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Journal.Migration.Migrations.DTOs;

public class VehicleTypeDTO
{
    public int vehicle_type_id { get; set; }

    public string? vehicle_type_name { get; set; }

    public bool is_active { get; set; }
}

public class VehicleBrandDTO
{
    public int vehicle_brand_id { get; set; }

    public string? vehicle_brand_name { get; set; }

    public int country_id { get; set; }
}
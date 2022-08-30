namespace Journal.Migration.Migrations.DTOs;

public class VehicleTypeDTO
{
    public int vehicle_type_id { get; set; }

    public string vehicle_type_name { get; set; }

    public bool is_active { get; set; }
}
namespace Journal.Domain.Models.Vehicle;

public class VehicleBrand
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int CountryId { get; set; }
}

public class Vehicle
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int TypeId { get; set; }

    public int BrandId { get; set; }
}
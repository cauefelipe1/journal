// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Journal.Migration.Migrations.DTOs;

public class UserTypeDTO
{
    public short user_type_id { get; set; }
    public string? user_type_desc { get; set; }
}
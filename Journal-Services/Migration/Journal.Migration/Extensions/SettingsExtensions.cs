namespace Journal.Migration.Extensions;

public static class SettingsExtensions
{
    public static string GetMasterDatabaseConnectionString(this SettingsData settings)
    {
        string str =
            $"Username={settings.Database.User};Password={settings.Database.Password};Host={settings.Database.Host};Port={settings.Database.Port};Database=postgres";
        return str;
    }
}
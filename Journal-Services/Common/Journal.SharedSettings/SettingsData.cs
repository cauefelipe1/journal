namespace Journal.SharedSettings;

/// <summary>
/// Defines all setting s used by all system
/// </summary>
public class SettingsData
{
    public DatabaseSettings Database { get; }

    public SettingsData()
    {
        Database = new DatabaseSettings();
    }
}

/// <summary>
/// Defines all properties used by the system to connect the database
/// </summary>
public class DatabaseSettings
{
    public string Host { get; set; } = default!;

    public int Port { get; set; }

    public string DatabaseName { get; set; } = default!;

    public string User { get; set; } = default!;

    public string Password { get; set; } = default!;

    private string _connectionString = default!;

    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = "";
            }

            return _connectionString;
        }
    }
}
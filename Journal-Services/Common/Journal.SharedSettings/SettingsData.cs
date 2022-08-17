using Microsoft.Extensions.Configuration;

namespace Journal.SharedSettings;

/// <summary>
/// Defines all setting s used by all system.
/// </summary>
public class SettingsData
{
    /// <summary>
    /// Database configuration section.
    /// <see cref="DatabaseSettings"/> for more details.
    /// </summary>
    public DatabaseSettings Database { get; }

    public SettingsData(IConfiguration configuration)
    {
        var dbSection = configuration.GetSection(nameof(Database));
        Database = new DatabaseSettings(dbSection);
    }
}

/// <summary>
/// Defines all properties used by the system to connect the database.
/// </summary>
public class DatabaseSettings
{
    /// <summary>
    /// Host address for the database.
    /// </summary>
    public string Host { get; } = default!;

    /// <summary>
    /// Port number the <see cref="Host"/> is listen to.
    /// </summary>
    public int Port { get; }

    /// <summary>
    /// Name of the database.
    /// </summary>
    public string DatabaseName { get; } = default!;

     /// <summary>
     /// User to connect to the database.
     /// </summary>
    public string User { get; } = default!;

    /// <summary>
    /// Password to connect to the database.
    /// </summary>
    public string Password { get; } = default!;

    /// <summary>
    /// Search path the database driver will use to locate the tables.
    /// </summary>
    public string SearchPath { get; } = default!;

    private string _connectionString = default!;

    /// <summary>
    /// Formatted connection string the driver will use to connect to the database.
    /// </summary>
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString =
                    $"Username={User};Password={Password};Host={Host};Port={Port};Database={DatabaseName};SearchPath={SearchPath}";
            }

            return _connectionString;
        }
    }

    public DatabaseSettings(IConfigurationSection configuration)
    {
        if (configuration is not null)
        {
            Host = configuration[nameof(Host)];
            Port = Convert.ToInt32(configuration[nameof(Port)]);
            DatabaseName = configuration[nameof(DatabaseName)];
            User = configuration[nameof(User)];
            Password = configuration[nameof(Password)];
            SearchPath = configuration[nameof(SearchPath)];
        }
    }
}
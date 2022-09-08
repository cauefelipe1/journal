// See https://aka.ms/new-console-template for more information

using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.VersionTableInfo;
using Journal.Migration.Migrations;
using Journal.Migration.Migrations.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var config = BuildConfiguration();
var builder = GetHostBuilder(config);
var server = builder.Build();

var set = server.Services.GetRequiredService<SettingsData>();
var createDb = new DatabaseHandler(set);
createDb.CheckOrCreateDatabase();

var mig = server.Services.GetRequiredService<IMigrationRunner>();

ShowInformDirectionMessage();

bool validInput = false;

do
{
    string input = Console.ReadLine();

    if (string.Equals(input, "up", StringComparison.InvariantCultureIgnoreCase))
    {
        var dbCreator = server.Services.GetRequiredService<DatabaseHandler>();
        dbCreator.CheckOrCreateDatabase();

        mig.MigrateUp();
        validInput = true;
    }
    else if (string.Equals(input, "rollback", StringComparison.InvariantCultureIgnoreCase))
    {
        mig.Rollback(1);
        validInput = true;
    }
    else if (string.Equals(input, "reset", StringComparison.InvariantCultureIgnoreCase))
    {
        var dbCreator = server.Services.GetRequiredService<DatabaseHandler>();
        dbCreator.ResetDatabase();

        mig.MigrateUp();
        validInput = true;
    }

    if (!validInput)
    {
        Console.WriteLine("Invalid option!");
        ShowInformDirectionMessage();
    }

} while (!validInput);


#region SupportMethods
void ShowInformDirectionMessage()
{
    Console.WriteLine("Inform the operation.");
    Console.WriteLine("Up, Rollback, Reset.");
}

IHostBuilder GetHostBuilder(IConfigurationRoot rootConfig)
{
    var hostBuilder = Host.CreateDefaultBuilder();

    hostBuilder.ConfigureServices((_, services) =>
    {
        var settings = services.AddSharedSettings(rootConfig);

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb =>
                rb.AddPostgres()
                    .WithGlobalConnectionString(settings.Database.ConnectionString)
                    .ScanIn(AppDomain.CurrentDomain.GetAssemblies()).For.Migrations())

            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .AddScoped<IVersionTableMetaData, CustomVersionTableMetaData>()
            .AddSingleton<IConventionSet, NamingConventionSet>()
            .AddSingleton<DatabaseHandler>();
    });

    return hostBuilder;
}

IConfigurationRoot BuildConfiguration()
{
    var configuration = new ConfigurationBuilder();

    const string SECRETS_FILE = "sharedSettings.Secrets.json";

    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
    configuration.AddJsonFile( Path.Combine(path, SECRETS_FILE), true, false);
    var conf = configuration.Build();

    return conf;
}
#endregion SupportMethods
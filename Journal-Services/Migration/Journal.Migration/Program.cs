// See https://aka.ms/new-console-template for more information

using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.VersionTableInfo;
using Journal.Migration.Migrations.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configuration = new ConfigurationBuilder();

const string SECRETS_FILE = "sharedSettings.Secrets.json";

string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
configuration.AddJsonFile( Path.Combine(path, SECRETS_FILE), true, false);
var config = configuration.Build();


var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices((_, services) =>
{
    var settings = services.AddSharedSettings(config);

    services
        .AddFluentMigratorCore()
        .ConfigureRunner(rb =>
            rb.AddPostgres()
                .WithGlobalConnectionString(settings.Database.ConnectionString)
                .ScanIn(AppDomain.CurrentDomain.GetAssemblies()).For.Migrations())

        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .AddScoped<IVersionTableMetaData, CustomVersionTableMetaData>()
        .AddSingleton<IConventionSet, NamingConventionSet>();
});

var server = builder.Build();

var mig = server.Services.GetRequiredService<IMigrationRunner>();

mig.MigrateUp();

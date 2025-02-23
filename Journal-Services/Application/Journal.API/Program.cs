using Journal.API.Configurations;
using Journal.API.DependencyInjection;
using Journal.Identity.Extensions;
using Journal.SharedSettings;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAdditionalConfigFiles();
builder.Configuration.AddEnvironmentVariables();
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwagger();

var settingsData = builder.Services.AddSharedSettings(configuration);
builder.Services.AddAppLocalization();
builder.Services.AddApplicationIdentity(configuration);
builder.Services.AddDatabase();
builder.Services.AddFeatures();

//Must be after AddFeatures
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


var app = builder.Build();

app.UseAppLocalization();

// Configure the HTTP request pipeline.
if (app.Environment.IsAnyDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{Constants.Swagger.GENERAL_API}/swagger.json", Constants.Swagger.GENERAL_API);
            c.SwaggerEndpoint($"/swagger/{Constants.Swagger.MOBILE_API}/swagger.json", Constants.Swagger.MOBILE_API);
        });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
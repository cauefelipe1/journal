using Journal.API.DependencyInjection;
using Journal.SharedSettings;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAdditionalConfigFiles();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwagger();

builder.Services.AddSharedSettings(builder.Configuration);
builder.Services.AddDatabase();
builder.Services.AddFeatures();

//Must be after AddFeatures
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
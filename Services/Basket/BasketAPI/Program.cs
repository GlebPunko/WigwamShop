using Basket.Application.DI;
using Basket.Infastructure.DI;
using BasketAPI.AppMiddleware;
using BasketAPI.Extensions;
using Serilog;
using ServiceExtension = BasketAPI.Extensions.ServiceExtension;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigurationAppsettings();

builder.Services.ConfigureKafka();

builder.Services.AddControllers();
builder.Services.ConfigureHangfire(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddData(builder.Configuration);
builder.Services.AddApplication();

ServiceExtension.ConfigureLogging(builder.Configuration);
builder.Host.UseSerilog();

var app = builder.Build();

await app.MigrateDatabaseAsync();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.SeedDataAsync();

app.UseApplicationExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomHangfireDashboard();

app.MapControllers();

app.Run();

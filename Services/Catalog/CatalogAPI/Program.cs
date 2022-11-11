using Serilog;
using Catalog.Application.DI;
using Catalog.Infastructure.DI;
using CatalogAPI.AppMiddleware;
using CatalogAPI.Extensions;
using ServiceExtensions = CatalogAPI.Extensions.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigurationAppsettings();

builder.Services.ConfigureKafka();
builder.Services.ConfigureHangfire(builder.Configuration);

ServiceExtensions.ConfigureLogging(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddData(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.MigrateDatabaseAsync();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseApplicationExceptionHandler();

app.UseAuthorization();

app.UseCustomHangfireDashboard();

app.MapControllers();

app.Run();

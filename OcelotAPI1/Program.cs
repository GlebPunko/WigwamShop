using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotAPI1.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.ConfigurationAppsettings();

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();
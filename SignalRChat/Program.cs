using SignalRChat.Hubs;
using SignalRChat.Models;
using SignalRChat.Extensions;
using SignalRChat.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigurationAppsettings();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCorsConfig();
builder.Services.AddHangfireConfig(builder.Configuration);

builder.Services.AddSingleton<IDictionary<string, UserConnection>>(options =>
    new Dictionary<string, UserConnection>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

await app.MigrateDatabaseAsync();

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chat");
});

app.UseCustomHangfireDashboard();

app.Run();

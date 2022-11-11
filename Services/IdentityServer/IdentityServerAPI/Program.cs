using IdentityServer.Application.DI;
using IdentityServerAPI;
using IdentityServerAPI.Extensions;
using TinyHelpers.Json.Serialization;

var corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigurationAppsettings();

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, b => { b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddApplication();

builder.Services.ConfigureIdentity();
builder.Services.ConfigureIdentityServer(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Initialize();
app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.UseCors(corsPolicy);

app.Run();

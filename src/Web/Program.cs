using DesafioEclipseworks.WebAPI.Extensions;
using DesafioEclipseworks.WebAPI.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddMediatR()
    .AddDbContext(configuration)
    .AddDI()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await AppContextSeed.SeedAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Eclipseworks.WebAPI V1");
    });
}

app.UseHttpsRedirection();

app.Run();
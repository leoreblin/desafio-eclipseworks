using DesafioEclipseworks.WebAPI.Abstractions.Data;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Services;
using DesafioEclipseworks.WebAPI.Infrastructure.Data;
using DesafioEclipseworks.WebAPI.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Desafio Eclipseworks Web API",
        Version = "v1",
        Description = "Eclispseworks RESTful API"
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration["ConnectionString"]);
});

builder.Services
    .AddTransient<IProjectRepository, ProjectRepository>()
    .AddTransient<ITaskRepository, TaskRepository>()
    .AddTransient<ITaskUpdateHistoryRepository, TaskUpdateHistoryRepository>()
    .AddTransient<IUnitOfWork, UnitOfWork>()
    .AddScoped<ReportService>();

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
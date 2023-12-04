using DesafioEclipseworks.WebAPI.Abstractions.Data;
using DesafioEclipseworks.WebAPI.Application.Reports;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Infrastructure.Data.Repositories;
using DesafioEclipseworks.WebAPI.Infrastructure.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DesafioEclipseworks.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(
            this IServiceCollection services)
        {
            return services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Desafio Eclipseworks Web API",
                        Version = "v1",
                        Description = "Eclispseworks RESTful API"
                    });
                });
        }

        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public static IServiceCollection AddDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(configuration["ConnectionString"]);
                });
        }

        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            return services
                .AddTransient<IProjectRepository, ProjectRepository>()
                .AddTransient<ITaskRepository, TaskRepository>()
                .AddTransient<ITaskUpdateHistoryRepository, TaskUpdateHistoryRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddScoped<ReportService>();
        }
    }
}

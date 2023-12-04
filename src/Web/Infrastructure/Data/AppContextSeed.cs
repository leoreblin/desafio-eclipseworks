using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Data
{
    public static class AppContextSeed
    {
        private static readonly Guid _managerUserId = new("44f596df-a5e2-4741-b047-0a0b5a4f26ba");
        private static readonly Guid _defaultProjectId = new("d6955201-8b1d-e811-80c2-000d3ab14b1a");

        public async static Task SeedAsync(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            if (!env.IsDevelopment())
            {
                return;
            }

            var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            await SeedUsersAsync(context);

            await SeedDefaultProjectAsync(context);

            await SeedTasksAsync(context);

            await context.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            User manager = new("Gerente", UserRole.Manager) { Id = _managerUserId };
            User common = new("Usuário Comum", UserRole.Common);

            await context.Users.AddRangeAsync(new List<User> { manager, common });
        }

        private static async Task SeedDefaultProjectAsync(AppDbContext context)
        {
            if (context.Projects.Any())
            {
                return;
            }

            Project project = new(_managerUserId, "Projeto Inicial");
            project.Id = _defaultProjectId;

            await context.Projects.AddAsync(project);
        }

        private static async Task SeedTasksAsync(AppDbContext context)
        {
            if (context.Tasks.Any())
            {
                return;
            }

            var tasks = new List<TaskEntity>
            {
                new TaskEntity(
                    title: "Tarefa A", details: "Detalhes da Tarefa A",
                    dueDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20)), status: Status.Pending,
                    priority: Priority.Low, projectId: _defaultProjectId),

                new TaskEntity(
                    title: "Tarefa B", details: "Detalhes da Tarefa B",
                    dueDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)), status: Status.InProgress,
                    priority: Priority.Medium, projectId: _defaultProjectId),

                new TaskEntity(
                    title: "Tarefa C", details: "Detalhes da Tarefa C",
                    dueDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20)), status: Status.Done,
                    priority: Priority.High, projectId: _defaultProjectId),
            };

            await context.Tasks.AddRangeAsync(tasks);
        }
    }
}

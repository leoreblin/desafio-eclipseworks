using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Entities.Users;
using DesafioEclipseworks.WebAPI.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskUpdateHistory> TaskUpdateHistory { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ProjectsEntityTypeConfiguration())
                .ApplyConfiguration(new TasksEntityTypeConfiguration())
                .ApplyConfiguration(new TaskUpdateHistoryEntityTypeConfiguration())
                .ApplyConfiguration(new UserEntityTypeConfiguration());

        }
    }
}

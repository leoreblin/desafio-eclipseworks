using DesafioEclipseworks.WebAPI.Data.EntityConfigurations;
using DesafioEclipseworks.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioEclipseworks.WebAPI.Data
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

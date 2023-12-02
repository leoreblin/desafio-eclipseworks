using DesafioEclipseworks.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioEclipseworks.WebAPI.Data.EntityConfigurations
{
    public class TasksEntityTypeConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.ToTable("Tasks");

            builder
                .HasKey(t => t.Id)
                .IsClustered(false)
                .HasName("PK_Tasks");

            builder
                .Property(t => t.Id)
                .ValueGeneratedNever()
                .HasDefaultValueSql("newsequentialid()")
                .IsRequired();

            builder
                .Property(t => t.Title)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder
                .Property(t => t.Details)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder
                .Property(t => t.DueDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(t => t.Status)
                .HasConversion<int>()
                .IsRequired();

            builder
                .Property(t => t.Priority)
                .HasConversion<int>()
                .IsRequired();

            builder
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(t => t.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Tasks_Project");

            builder
                .HasIndex(t => new
                {
                    t.Status,
                    t.DueDate,
                    t.Id
                })
                .IncludeProperties(t => new
                {
                    t.Details,
                    t.Title
                })
                .HasDatabaseName("IDX_TASKS_ID_STATUS_DUEDATE");
        }
    }
}

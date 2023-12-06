using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Data.EntityConfigurations
{
    public class TaskUpdateHistoryEntityTypeConfiguration : IEntityTypeConfiguration<TaskUpdateHistory>
    {
        public void Configure(EntityTypeBuilder<TaskUpdateHistory> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ToTable("TaskUpdateHistory");

            builder
                .HasKey(t => t.Id)
                .IsClustered(false)
                .HasName("PK_TaskUpdateHistory");

            builder
                .Property(t => t.Id)
                .ValueGeneratedNever()
                .HasDefaultValueSql("newsequentialid()")
                .IsRequired();

            builder
                .Property(t => t.UpdatedDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(t => t.Comment)
                .HasColumnType("nvarchar(200)")
                .IsRequired(false);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.UpdatedBy)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TaskUpdateHistory_User");

            builder
                .HasOne<TaskEntity>()
                .WithMany()
                .HasForeignKey(t => t.TaskId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TaskUpdateHistory_Task");

            builder
                .HasIndex(t => t.Id)
                .IncludeProperties(t => new { t.UpdatedDate, t.UpdatedBy })
                .HasDatabaseName("IDX_TASKUPDATEHISTORY_ID_UPDATEDBY_UPDATEDDATE");
        }
    }
}

using DesafioEclipseworks.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioEclipseworks.WebAPI.Data.EntityConfigurations
{
    public class ProjectsEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.ToTable("Projects");

            builder
                .HasKey(p => p.Id)
                .IsClustered(false)
                .HasName("PK_Projects");

            builder
                .Property(p => p.Id)
                .ValueGeneratedNever()
                .HasDefaultValueSql("newsequentialid()")
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder
                .HasIndex(p => p.Id)
                .IncludeProperties(p => p.Name)
                .HasDatabaseName("IDX_PROJECTS_ID");

            builder
                .HasIndex(p => p.Name)
                .IncludeProperties(p => p.Id)
                .HasDatabaseName("IDX_PROJECTS_NAME");
        }
    }
}

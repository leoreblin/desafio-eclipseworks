using DesafioEclipseworks.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioEclipseworks.WebAPI.Data.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.ToTable("Users");

            builder
                .HasKey(u => u.Id)
                .IsClustered(false)
                .HasName("PK_User");

            builder
                .Property(u => u.Id)
                .ValueGeneratedNever()
                .HasDefaultValueSql("newsequentialid()")
                .IsRequired();

            builder
                .Property(u => u.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(u => u.Role)
                .HasConversion<int>()
                .IsRequired();

            builder
                .HasIndex(t => t.Id)
                .IncludeProperties(t => new { t.Name, t.Role})
                .HasDatabaseName("IDX_USER_NAME_ROLE");
        }
    }
}

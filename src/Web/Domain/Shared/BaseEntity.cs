using System.ComponentModel.DataAnnotations;

namespace DesafioEclipseworks.WebAPI.Domain.Shared
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}

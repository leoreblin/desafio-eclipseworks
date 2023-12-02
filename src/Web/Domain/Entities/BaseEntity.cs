using System.ComponentModel.DataAnnotations;

namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}

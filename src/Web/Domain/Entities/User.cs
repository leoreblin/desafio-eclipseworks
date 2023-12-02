using DesafioEclipseworks.WebAPI.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = default!;
        public UserRole Role { get; set; }
    }
}

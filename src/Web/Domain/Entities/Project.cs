using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = default!;
    }
}

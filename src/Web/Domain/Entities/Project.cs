using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;
    }
}

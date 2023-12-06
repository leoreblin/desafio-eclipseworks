using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Domain.Entities.Projects
{
    public class Project : BaseEntity
    {
        public string Name { get; } = default!;

        public IReadOnlyCollection<TaskEntity> Tasks { get; set; } = default!;

        public Guid UserId { get; }

        public Project(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}

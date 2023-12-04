using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Domain.Entities.Tasks
{
    public class TaskEntity : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Details { get; set; } = default!;
        public DateOnly DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; }
        public Guid ProjectId { get; set; }

        public TaskEntity(string title, string details, DateOnly dueDate, Status status, Priority priority, Guid projectId)
        {
            Title = title;
            Details = details;
            DueDate = dueDate;
            Status = status;
            Priority = priority;
            ProjectId = projectId;
        }
    }
}

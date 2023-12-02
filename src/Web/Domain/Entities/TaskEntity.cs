using DesafioEclipseworks.WebAPI.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Details { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; }
        public Guid ProjectId { get; set; }

        public TaskEntity(string title, string details, DateTime dueDate, Status status, Priority priority)
        {
            Title = title;
            Details = details;
            DueDate = dueDate;
            Status = status;
            Priority = priority;
        }
    }
}

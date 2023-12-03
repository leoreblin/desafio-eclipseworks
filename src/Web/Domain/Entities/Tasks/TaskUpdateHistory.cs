using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Domain.Entities.Tasks
{
    public class TaskUpdateHistory : BaseEntity
    {
        public Guid TaskId { get; }

        public Guid UpdatedBy { get; }

        public DateTime UpdatedDate { get; }

        public string Comment { get; } = default!;

        public TaskUpdateHistory(Guid taskId, Guid updatedBy, DateTime updatedDate, string comment)
        {
            TaskId = taskId;
            UpdatedBy = updatedBy;
            UpdatedDate = updatedDate;
            Comment = comment;
        }
    }
}

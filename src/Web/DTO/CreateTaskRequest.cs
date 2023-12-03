using DesafioEclipseworks.WebAPI.Application.Tasks.Create;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;

namespace DesafioEclipseworks.WebAPI.DTO
{
    public record CreateTaskRequest
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }

        public static implicit operator CreateTaskCommand(CreateTaskRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            return new CreateTaskCommand(
                request.ProjectId,
                request.Title,
                request.Details,
                request.DueDate,
                request.Status,
                request.Priority);
        }
    }
}

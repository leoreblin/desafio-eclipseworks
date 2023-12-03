using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.Create
{
    public sealed record CreateTaskCommand(
        Guid ProjectId,
        string Title,
        string Details,
        DateTime DueDate,
        Status Status,
        Priority Priority) : ICommand
    {
        public static implicit operator TaskEntity(CreateTaskCommand request)
        {
            ArgumentNullException.ThrowIfNull(request);

            return new TaskEntity(
                title: request.Title,
                details: request.Details,
                dueDate: request.DueDate,
                status: request.Status,
                priority: request.Priority,
                projectId: request.ProjectId);
        }
    }
}

using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.DTO;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.Create
{
    public sealed record CreateTaskCommand(
        Guid ProjectId,
        CreateTaskRequest Request) : ICommand
    {
        public static implicit operator TaskEntity(CreateTaskCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            return new TaskEntity(
                title: command.Request.Title,
                details: command.Request.Details,
                dueDate: DateOnly.FromDateTime(command.Request.DueDate),
                status: Status.Pending,
                priority: command.Request.Priority,
                projectId: command.ProjectId);
        }
    }
}

using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.Update
{
    public record UpdateTaskCommand(
        Guid UserId,
        Guid TaskId,
        string Title,
        string Details,
        DateTime DueDate,
        Status Status) : ICommand;
}

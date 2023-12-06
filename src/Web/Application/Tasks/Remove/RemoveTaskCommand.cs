using DesafioEclipseworks.WebAPI.Abstractions.Messaging;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.Remove
{
    public sealed record RemoveTaskCommand(Guid TaskId) : ICommand;
}

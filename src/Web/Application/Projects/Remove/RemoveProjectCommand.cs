using DesafioEclipseworks.WebAPI.Abstractions.Messaging;

namespace DesafioEclipseworks.WebAPI.Application.Projects.Remove
{
    public sealed record RemoveProjectCommand(Guid ProjectId) : ICommand;
}

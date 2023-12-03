using DesafioEclipseworks.WebAPI.Abstractions.Messaging;

namespace DesafioEclipseworks.WebAPI.Application.Projects.Create
{
    public sealed record CreateProjectCommand(Guid UserId, string ProjectName) : ICommand;
}

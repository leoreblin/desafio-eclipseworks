using DesafioEclipseworks.WebAPI.Abstractions.Messaging;

namespace DesafioEclipseworks.WebAPI.Application.Projects.Commands
{
    public sealed record CreateProjectCommand(string ProjectName) : ICommand;
}

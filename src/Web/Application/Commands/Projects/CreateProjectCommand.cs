using DesafioEclipseworks.WebAPI.Abstractions.Messaging;

namespace DesafioEclipseworks.WebAPI.Application.Commands.Projects
{
    public sealed record CreateProjectCommand(string ProjectName) : ICommand
    {
        
    }
}

using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Application.Projects.Errors;
using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Application.Projects.Commands
{
    public sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ProjectName))
            {
                return ProjectErrors.ProjectNameIsNullOrEmpty;
            }

            return Result.Success();
        }
    }
}

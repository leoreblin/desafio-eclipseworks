using DesafioEclipseworks.WebAPI.Abstractions.Data;
using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Shared;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;

namespace DesafioEclipseworks.WebAPI.Application.Projects.Remove
{
    public class RemoveProjectCommandHandler : ICommandHandler<RemoveProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            RemoveProjectCommand request,
            CancellationToken cancellationToken)
        {
            var projectFromDb = await _projectRepository.GetProjectAsync(request.ProjectId);

            if (projectFromDb is null)
            {
                return ProjectErrors.ProjectDoesNotExist(request.ProjectId);
            }

            var projectTasks = projectFromDb.Tasks;

            if (projectTasks.Any(t => t.Status == Status.Pending))
            {
                return ProjectErrors.ProjectWithPendingTasks(request.ProjectId);
            }

            _projectRepository.RemoveProject(projectFromDb);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

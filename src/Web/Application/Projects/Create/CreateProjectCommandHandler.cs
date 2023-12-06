using DesafioEclipseworks.WebAPI.Abstractions.Data;
using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Shared;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;

namespace DesafioEclipseworks.WebAPI.Application.Projects.Create
{
    public sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            CreateProjectCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ProjectName))
            {
                return ProjectErrors.ProjectNameIsNullOrEmpty;
            }

            Project newProject = new(request.UserId, request.ProjectName);

            await _projectRepository.AddProjectAsync(newProject);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(newProject.Id);
        }
    }
}

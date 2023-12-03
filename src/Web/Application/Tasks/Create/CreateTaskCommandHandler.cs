using DesafioEclipseworks.WebAPI.Abstractions.Data;
using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Shared;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.Create
{
    public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskCommandHandler(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            CreateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var projectFromDb = await _projectRepository.GetProjectAsync(request.ProjectId);

            if (projectFromDb is null)
            {
                return TaskErrors.ProjectDoesNotExist(request.ProjectId);
            }

            if (projectFromDb.Tasks.Count == 20)
            {
                return TaskErrors.ProjectWithTaskLimitMaximum(request.ProjectId);
            }

            TaskEntity newTask = request;

            await _taskRepository.CreateTaskAsync(newTask);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

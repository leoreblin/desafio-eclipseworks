using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Shared;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.GetAll
{
    public class GetAllTasksQueryHandler : IQueryHandler<GetAllTasksQuery, Result<List<TaskEntity>?>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public GetAllTasksQueryHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<Result<List<TaskEntity>?>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectAsync(request.ProjectId);

            if (project is null)
            {
                return TaskErrors.ProjectDoesNotExist(request.ProjectId);
            }

            var tasks = await _taskRepository.GetAllTasksByProjectIdAsync(request.ProjectId);

            return tasks;
        }
    }
}

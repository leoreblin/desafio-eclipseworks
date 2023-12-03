using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Application.Projects.GetAll
{
    public sealed class GetAllProjectsQueryHandler
        : IQueryHandler<GetAllProjectsQuery, Result<List<Project>?>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<List<Project>?>> Handle(
            GetAllProjectsQuery request,
            CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllUserProjectsAsync(request.UserId);

            return projects;
        }
    }
}

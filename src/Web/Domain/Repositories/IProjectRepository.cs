using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;

namespace DesafioEclipseworks.WebAPI.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectAsync(Guid id);

        Task<List<Project>?> GetAllUserProjectsAsync(Guid userId);

        Task CreateProjectAsync(Project project);

        void RemoveProject(Project project);
    }
}

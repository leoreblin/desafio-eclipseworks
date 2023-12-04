using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateProjectAsync(Project project)
        {
            await _context.AddAsync(project);
        }

        public async Task<List<Project>?> GetAllUserProjectsAsync(Guid userId)
        {
            return await _context.Projects
                .Where(p => p.UserId == userId)
                .Include(p => p.Tasks)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectAsync(Guid id)
        {
            return await _context.Projects
                .Where(p => p.Id == id)
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync();
        }

        public void RemoveProject(Project project)
        {
            _context.Remove(project);
        }
    }
}

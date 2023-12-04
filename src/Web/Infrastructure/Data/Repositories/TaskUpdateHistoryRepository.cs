using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Data.Repositories
{
    public class TaskUpdateHistoryRepository : ITaskUpdateHistoryRepository
    {
        private readonly AppDbContext _context;

        public TaskUpdateHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateHistoryAsync(TaskUpdateHistory history)
        {
            await _context.AddAsync(history);
        }

        public async Task<List<TaskUpdateHistory>?> GetTaskUpdateHistoriesOverLast30Days()
        {
            return await _context.TaskUpdateHistory
                .AsNoTracking()
                .Where(t => t.UpdatedDate > DateTime.Now.AddDays(-30))
                .ToListAsync();
        }
    }
}

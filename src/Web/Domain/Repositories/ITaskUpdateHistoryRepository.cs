using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;

namespace DesafioEclipseworks.WebAPI.Domain.Repositories
{
    public interface ITaskUpdateHistoryRepository
    {
        Task CreateHistoryAsync(TaskUpdateHistory history);
        Task<List<TaskUpdateHistory>?> GetTaskUpdateHistoriesOverLast30Days();
    }
}

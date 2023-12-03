using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;

namespace DesafioEclipseworks.WebAPI.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskEntity?> GetTaskAsync(Guid id);

        Task<List<TaskEntity>?> GetAllTasksByProjectIdAsync(Guid projectId);

        Task CreateTaskAsync(TaskEntity task);

        void UpdateTaskAsync(TaskEntity task);

        void RemoveTaskAsync(TaskEntity task);
    }
}

using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;

namespace DesafioEclipseworks.WebAPI.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskEntity?> GetTaskAsync(Guid id);

        Task<List<TaskEntity>?> GetAllTasksByProjectIdAsync(Guid projectId);

        Task<List<TaskEntity>?> GetAllCompletedTasksAsync();

        Task AddTaskAsync(TaskEntity task);

        void UpdateTask(TaskEntity task);

        void RemoveTask(TaskEntity task);
    }
}

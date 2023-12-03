using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Errors
{
    public static class TaskErrors
    {
        public static Error ProjectDoesNotExist(Guid projectId) => 
            new("Task.Create", $"O projeto cujo ID é {projectId} não existe.");

        public static Error ProjectWithTaskLimitMaximum(Guid projectId) =>
            new("Task.Create", $"O projeto cujo ID é {projectId} já possui 20 tarefas. Portanto, não será possível criar mais uma.");

        public static Error TaskDoesNotExist(Guid taskId) =>
            new("Task.Update", $"O ID {taskId} não pertence a nenhuma Tarefa.");
    }
}

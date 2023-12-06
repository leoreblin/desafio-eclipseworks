using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Errors
{
    public static class TaskErrors
    {
        public static Error ProjectDoesNotExist(Guid projectId) => 
            new("Task.Create", $"O projeto de ID {projectId} não existe.");

        public static Error ProjectWithTaskLimitMaximum(Guid projectId) =>
            new("Task.Create", $"O projeto de ID {projectId} já atingiu o limite máximo de 20 tarefas.");

        public static Error TaskDoesNotExist(Guid taskId) =>
            new("Task.Update", $"O ID {taskId} não pertence a nenhuma Tarefa.");

        public static readonly Error InvalidDueDate =
            new("Task", $"A data de vencimento não pode ser menor que a data atual.");
    }
}

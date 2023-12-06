using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Errors
{
    public static class ProjectErrors
    {
        public static readonly Error ProjectNameIsNullOrEmpty = new("Project.Create", "O nome do projeto não pode ser nulo ou vazio.");

        public static Error ProjectDoesNotExist(Guid projectId) => new(
            "Project",
            $"Não existe nenhum projeto de ID {projectId}.");

        public static Error ProjectWithPendingTasks(Guid projectId) => new(
            "Project.Remove",
            $"O projeto de ID {projectId} possui tarefas pendentes. Para removê-lo, favor remover ou conclui-las.");
    }
}

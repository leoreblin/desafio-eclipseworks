using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Errors
{
    public static class ProjectErrors
    {
        public static readonly Error ProjectNameIsNullOrEmpty = new("Project.Create", "O nome do projeto não pode ser nulo ou vazio.");
    }
}

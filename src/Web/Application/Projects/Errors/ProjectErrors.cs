using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Application.Projects.Errors
{
    public static class ProjectErrors
    {
        public static readonly Error ProjectNameIsNullOrEmpty = new("CreateProjectCommand", "O nome do projeto não pode ser nulo ou vazio.");
    }
}

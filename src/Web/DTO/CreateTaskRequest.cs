using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;

namespace DesafioEclipseworks.WebAPI.DTO
{
    public record CreateTaskRequest(string Title, string Details, DateTime DueDate, Priority Priority);
}

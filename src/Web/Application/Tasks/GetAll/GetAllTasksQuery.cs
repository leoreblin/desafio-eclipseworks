using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.GetAll
{
    public sealed record GetAllTasksQuery(Guid ProjectId) : IQuery<Result<List<TaskEntity>?>>;
}

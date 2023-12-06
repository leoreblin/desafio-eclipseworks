using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Application.Projects.GetAll
{
    public sealed record GetAllProjectsQuery(Guid UserId) : IQuery<Result<List<Project>?>>;
}

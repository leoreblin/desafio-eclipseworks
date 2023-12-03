using DesafioEclipseworks.WebAPI.Application.Projects.Create;
using DesafioEclipseworks.WebAPI.Application.Projects.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioEclipseworks.WebAPI.Controllers
{
    public sealed class ProjectsController : ApiController
    {
        public ProjectsController(ISender sender) : base(sender)
        {   
        }

        [HttpGet("api/v1/users/{userId}/projects")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllProjects(Guid userId)
        {
            var query = new GetAllProjectsQuery(userId);

            var result = await Sender.Send(query);

            return Ok(result.Value);
        }

        [HttpPost("api/v1/users/{userId}/projects/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateProject(
            Guid userId,
            string projectName,
            CancellationToken cancellationToken)
        {
            var request = new CreateProjectCommand(userId, projectName);

            var result = await Sender.Send(request, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete("api/v1/users/projects/{projectId}/remove")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RemoveProject(
            Guid projectId,
            CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}

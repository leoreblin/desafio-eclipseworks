using DesafioEclipseworks.WebAPI.Application.Projects.Commands;
using DesafioEclipseworks.WebAPI.DTO;
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

        [HttpGet("api/v1/projects")]
        public async Task<IActionResult> GetAllUserProjects()
        {
            return Ok();
        }

        [HttpPost("api/v1/projects/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateProject(
            CreateProjectRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateProjectCommand(request.ProjectName);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result);
        }
    }
}

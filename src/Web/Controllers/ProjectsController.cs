using DesafioEclipseworks.WebAPI.Application.Commands.Projects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEclipseworks.WebAPI.Controllers
{
    [Route("api/v1/projects")]
    public sealed class ProjectsController : ApiController
    {
        public ProjectsController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserProjects(Guid userId)
        {
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string projectName, CancellationToken cancellationToken)
        {
            var command = new CreateProjectCommand(projectName);

            var result = await Sender.Send(command, cancellationToken);

            return Ok();
        }
    }
}

using DesafioEclipseworks.WebAPI.Application.Projects.GetAll;
using DesafioEclipseworks.WebAPI.Application.Tasks.Create;
using DesafioEclipseworks.WebAPI.Application.Tasks.Remove;
using DesafioEclipseworks.WebAPI.Application.Tasks.Update;
using DesafioEclipseworks.WebAPI.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioEclipseworks.WebAPI.Controllers
{
    [ApiController]
    public sealed class TasksController : ApiController
    {
        public TasksController(ISender sender) : base(sender)
        {
        }

        [HttpGet("api/v1/projects/{projectId}/tasks")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllProjectTasks(Guid projectId)
        {
            var query = new GetAllProjectsQuery(projectId);

            var result = await Sender.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("api/v1/projects/{projectId}/tasks/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateTask(
            Guid projectId,
            CreateTaskRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateTaskCommand(projectId, request);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.EntityId);
        }

        [HttpPut("api/v1/tasks/update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateTask(
            UpdateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete("api/v1/tasks/{taskId}/remove")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RemoveTask(Guid taskId, CancellationToken cancellationToken)
        {
            var command = new RemoveTaskCommand(taskId);

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}

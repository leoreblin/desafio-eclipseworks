using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEclipseworks.WebAPI.Controllers
{
    [ApiController]
    public sealed class TasksController : ApiController
    {
        public TasksController(ISender sender) : base(sender)
        {
        }

        [HttpGet("api/v1/tasks")]
        public async Task<IActionResult> GetAllProjectTasks(Guid projectId)
        {
            return Ok();
        }

        [HttpPost("api/v1/tasks/{projectId}/create")]
        public async Task<IActionResult> CreateTask(Guid projectId)
        {
            return Ok();
        }

        [HttpPut("api/v1/tasks/{taskId}/update")]
        public async Task<IActionResult> UpdateTask(Guid taskId)
        {
            return Ok();
        }

        [HttpDelete("api/v1/tasks/{taskId}/remove")]
        public async Task<IActionResult> RemoveTask(Guid taskId)
        {
            return Ok();
        }
    }
}

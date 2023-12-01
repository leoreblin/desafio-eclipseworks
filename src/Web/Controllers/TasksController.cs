using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEclipseworks.WebAPI.Controllers
{
    [Route("api/v1/tasks")]
    public sealed class TasksController : ApiController
    {
        public TasksController(ISender sender) : base(sender)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjectTasks(Guid projectId)
        {
            return Ok();
        }

        [HttpPost("{projectId}/create")]
        public async Task<IActionResult> CreateTask(Guid projectId)
        {
            return Ok();
        }

        [HttpPut("{taskId}/update")]
        public async Task<IActionResult> UpdateTask(Guid taskId)
        {
            return Ok();
        }

        [HttpDelete("{taskId}/remove")]
        public async Task<IActionResult> RemoveTask(Guid taskId)
        {
            return Ok();
        }
    }
}

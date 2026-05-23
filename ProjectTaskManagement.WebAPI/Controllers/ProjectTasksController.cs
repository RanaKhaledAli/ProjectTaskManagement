using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Core.DTOs.Tasks;
using ProjectTaskManagement.Core.ServiceContracts;

namespace ProjectTaskManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectTasksController : BaseApiController
    {
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTasksController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }

        [HttpPost("~/api/projects/{projectId:guid}/tasks")]
        [ProducesResponseType(typeof(ProjectTaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(Guid projectId, [FromBody] CreateProjectTaskDto createTaskDto)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            var task = await _projectTaskService.CreateAsync(projectId, createTaskDto, CurrentUserId);

            return Ok(task);
        }

        [HttpGet("~/api/projects/{projectId:guid}/tasks")]
        [ProducesResponseType(typeof(IEnumerable<ProjectTaskDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByProject(Guid projectId)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            var tasks = await _projectTaskService.GetByProjectAsync(projectId, CurrentUserId);

            return Ok(tasks);
        }

        [HttpPatch("~/api/tasks/{taskId:guid}/status")]
        [ProducesResponseType(typeof(ProjectTaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(Guid taskId, [FromBody] UpdateProjectTaskStatusDto updateStatusDto)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            var task = await _projectTaskService.UpdateStatusAsync(taskId, updateStatusDto, CurrentUserId);

            return Ok(task);
        }

        [HttpDelete("~/api/tasks/{taskId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            await _projectTaskService.DeleteAsync(taskId, CurrentUserId);

            return NoContent();
        }
    }
}



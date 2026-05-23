using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Core.DTOs.Projects;
using ProjectTaskManagement.Core.ServiceContracts;

namespace ProjectTaskManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : BaseApiController
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto createProjectDto)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            var project = await _projectService.CreateAsync(createProjectDto, CurrentUserId);

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            var projects = await _projectService.GetAllAsync(CurrentUserId);

            return Ok(projects);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            var project = await _projectService.GetByIdAsync(id, CurrentUserId);

            return Ok(project);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            var project = await _projectService.UpdateAsync(id, updateProjectDto, CurrentUserId);

            return Ok(project);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (string.IsNullOrWhiteSpace(CurrentUserId))
            {
                return Unauthorized();
            }

            await _projectService.DeleteAsync(id, CurrentUserId);

            return NoContent();
        }
    }
}
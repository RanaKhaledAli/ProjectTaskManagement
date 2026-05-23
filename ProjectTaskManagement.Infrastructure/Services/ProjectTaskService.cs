using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Core.Domain.Entities;
using ProjectTaskManagement.Core.DTOs.Tasks;
using ProjectTaskManagement.Core.ServiceContracts;
using ProjectTaskManagement.Infrastructure.DbContext;

namespace ProjectTaskManagement.Infrastructure.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectTaskService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectTaskDto> CreateAsync(Guid projectId, CreateProjectTaskDto createTaskDto, string userId)
        {
            var projectExists = await _dbContext.Projects
                .AnyAsync(p => p.Id == projectId && p.UserId == userId);

            if (!projectExists)
            {
                throw new KeyNotFoundException("Project not found");
            }

            var task = new ProjectTask
            {
                Title = createTaskDto.Title.Trim(),
                Description = createTaskDto.Description,
                DueDate = createTaskDto.DueDate,
                Priority = createTaskDto.Priority,
                ProjectId = projectId
            };

            await _dbContext.ProjectTasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return MapToDto(task);
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetByProjectAsync(Guid projectId, string userId)
        {
            var projectExists = await _dbContext.Projects
                .AnyAsync(p => p.Id == projectId && p.UserId == userId);

            if (!projectExists)
            {
                throw new KeyNotFoundException("Project not found");
            }

            return await _dbContext.ProjectTasks
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .Select(t => new ProjectTaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    ProjectId = t.ProjectId
                })
                .ToListAsync();
        }

        public async Task<ProjectTaskDto> UpdateStatusAsync(Guid taskId, UpdateProjectTaskStatusDto updateStatusDto, string userId)
        {
            var task = await _dbContext.ProjectTasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.UserId == userId);

            if (task is null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            task.Status = updateStatusDto.Status;

            await _dbContext.SaveChangesAsync();

            return MapToDto(task);
        }

        public async Task DeleteAsync(Guid taskId, string userId)
        {
            var task = await _dbContext.ProjectTasks
                .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.UserId == userId);

            if (task is null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            _dbContext.ProjectTasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        private static ProjectTaskDto MapToDto(ProjectTask task)
        {
            return new ProjectTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                Priority = task.Priority,
                ProjectId = task.ProjectId
            };
        }
    }
}
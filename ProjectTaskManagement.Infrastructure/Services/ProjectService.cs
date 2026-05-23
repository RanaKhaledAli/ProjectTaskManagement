using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Core.Domain.Entities;
using ProjectTaskManagement.Core.DTOs.Projects;
using ProjectTaskManagement.Core.ServiceContracts;
using ProjectTaskManagement.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManagement.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto createProjectDto, string userId)
        {
            var project = new Project
            {
                Name = createProjectDto.Name.Trim(),
                Description = createProjectDto.Description,
                UserId = userId
            };

            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return MapToDto(project);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync(string userId)
        {
            return await _dbContext.Projects
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ProjectDto> GetByIdAsync(Guid id, string userId)
        {
            var project = await _dbContext.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (project is null)
            {
                throw new KeyNotFoundException("Project not found");
            }

            return MapToDto(project);
        }

        public async Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto updateProjectDto, string userId)
        {
            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (project is null)
            {
                throw new KeyNotFoundException("Project not found");
            }

            project.Name = updateProjectDto.Name.Trim();
            project.Description = updateProjectDto.Description;

            await _dbContext.SaveChangesAsync();

            return MapToDto(project);
        }

        public async Task DeleteAsync(Guid id, string userId)
        {
            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (project is null)
            {
                throw new KeyNotFoundException("Project not found");
            }

            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();
        }

        private static ProjectDto MapToDto(Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }
    }
}

using ProjectTaskManagement.Core.DTOs.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManagement.Core.ServiceContracts
{
    public interface IProjectService
    {
         

        Task<ProjectDto> CreateAsync(CreateProjectDto createProjectDto, string userId);

        Task<IEnumerable<ProjectDto>> GetAllAsync(string userId);

        Task<ProjectDto> GetByIdAsync(Guid id, string userId);

        Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto updateProjectDto, string userId);

        Task DeleteAsync(Guid id, string userId);

    }
}

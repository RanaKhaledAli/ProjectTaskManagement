using ProjectTaskManagement.Core.DTOs.Tasks;

namespace ProjectTaskManagement.Core.ServiceContracts
{
    public interface IProjectTaskService
    {
        Task<ProjectTaskDto> CreateAsync(Guid projectId, CreateProjectTaskDto createTaskDto, string userId);

        Task<IEnumerable<ProjectTaskDto>> GetByProjectAsync(Guid projectId, string userId);

        Task<ProjectTaskDto> UpdateStatusAsync(Guid taskId, UpdateProjectTaskStatusDto updateStatusDto, string userId);

        Task DeleteAsync(Guid taskId, string userId);
    }
}
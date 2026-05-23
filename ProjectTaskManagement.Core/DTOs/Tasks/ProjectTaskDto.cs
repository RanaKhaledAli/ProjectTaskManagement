using ProjectTaskManagement.Core.Enums;

namespace ProjectTaskManagement.Core.DTOs.Tasks
{
    public class ProjectTaskDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ProjectTaskStatus Status { get; set; }

        public DateTime? DueDate { get; set; }

        public ProjectTaskPriority Priority { get; set; }

        public Guid ProjectId { get; set; }
    }
}
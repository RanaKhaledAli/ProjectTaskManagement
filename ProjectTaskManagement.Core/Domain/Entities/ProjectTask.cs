using ProjectTaskManagement.Core.Enums;
namespace ProjectTaskManagement.Core.Domain.Entities
{
    public class ProjectTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Pending;

        public DateTime? DueDate { get; set; }

        public ProjectTaskPriority Priority { get; set; } = ProjectTaskPriority.Medium;

        public Guid ProjectId { get; set; }

        public Project Project { get; set; } = null!;
    }
}


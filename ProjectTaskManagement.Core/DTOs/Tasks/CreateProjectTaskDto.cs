using ProjectTaskManagement.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectTaskManagement.Core.DTOs.Tasks
{
    public class CreateProjectTaskDto
    {
        [Required(ErrorMessage = "Task title is required")]
        [StringLength(200, ErrorMessage = "Task title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public ProjectTaskPriority Priority { get; set; } = ProjectTaskPriority.Medium;
    }
}
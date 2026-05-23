using ProjectTaskManagement.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectTaskManagement.Core.DTOs.Tasks
{
    public class UpdateProjectTaskStatusDto
    {
        [Required(ErrorMessage = "Task status is required")]
        public ProjectTaskStatus Status { get; set; }
    }
}
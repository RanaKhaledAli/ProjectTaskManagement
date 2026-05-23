using Microsoft.AspNetCore.Identity;


namespace ProjectTaskManagement.Core.Domain.Entities.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public ICollection<Project> Projects { get; set; } = new List<Project>();


    }
}

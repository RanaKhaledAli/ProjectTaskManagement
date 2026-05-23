using ProjectTaskManagement.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManagement.Core.ServiceContracts
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user, DateTime expiration);
    }
}

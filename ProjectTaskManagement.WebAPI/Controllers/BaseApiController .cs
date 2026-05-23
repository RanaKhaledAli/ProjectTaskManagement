using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjectTaskManagement.WebAPI.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected string? CurrentUserId =>
            User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

}

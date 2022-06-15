using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMWebHost.Controllers
{
    [Authorize]
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : Controller
    {
        [HttpGet("home")]
        public IActionResult Statistics()
        {
            return Ok(new { isSuccess = true, message = "welcome to home screen." });
        }
    }
}

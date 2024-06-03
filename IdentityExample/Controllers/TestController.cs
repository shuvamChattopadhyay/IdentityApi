using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("AdminOnly")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdminOnly()
        {
             return Ok("Your Role is Admin");
        }

        [HttpGet]
        [Route("AdminAndSuperOnly")]
        [Authorize(Roles = "Admin,Super_Admin")]
        public async Task<IActionResult> AdminAndSuperOnly()
        {
            return Ok("Your Role is Admin or Super Admin");
        }

        [HttpGet]
        [Route("SuperOnly")]
        [Authorize(Roles = "Super_Admin")]
        public async Task<IActionResult> SuperOnly()
        {
            return Ok("Your Role is Super Admin");
        }

        [HttpGet]
        [Route("BackofficeOnly")]
        [Authorize(Roles = "Back_Office")]
        public async Task<IActionResult> BackofficeOnly()
        {
            return Ok("Your Role is Back office");
        }
    }
}

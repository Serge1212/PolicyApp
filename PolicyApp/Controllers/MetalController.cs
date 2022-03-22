using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolicyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetalController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Authorize("ShouldBeAMetalFan")]
        public IActionResult Get()
        {
            return Ok("some data");
        }
    }
}

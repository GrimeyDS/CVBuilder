using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Controllers
{
    [Route("")]
    [Authorize]
    [ApiController]
    public class CVBuilderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to the CVBuilder API");
        }
    }
}

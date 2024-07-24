using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionHandlingTestController : ControllerBase
    {
        [HttpGet("trigger-exception")]
        public IActionResult TriggerException()
        {
            throw new Exception("This is a test exception!");
        }
    }
}

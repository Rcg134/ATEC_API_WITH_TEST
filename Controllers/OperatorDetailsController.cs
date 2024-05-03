using Microsoft.AspNetCore.Mvc;

namespace ATEC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperatorDetailsController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> ReturnOk()
        {

            return BadRequest();
        }
    }
}
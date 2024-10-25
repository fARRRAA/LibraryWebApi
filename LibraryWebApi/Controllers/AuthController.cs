using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("registerReader")]
        public async Task<ActionResult> Register([FromBody] createReader reader)
        {
            return null;
        }
        [HttpGet("loginReader")]
        public async Task<ActionResult> Login(string login, string password)
        {
            return null;
        }
        [HttpGet("userData/{token}")]
        public async Task<IActionResult> GetUserData(string token)
        {
            return null;
        }
    }
}

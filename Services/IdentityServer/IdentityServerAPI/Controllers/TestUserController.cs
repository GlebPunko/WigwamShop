using IdentityServer.Application.Interfaces;
using IdentityServer.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestUserController : ControllerBase
    {
        private static readonly string SecretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJOMZ4zY";
        private static readonly string Issuer = "http://localhost:5103";
        private static readonly string Audience = "http://localhost:5103";
        private readonly IAuthService _service;

        public TestUserController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (await _service.RegisterAsync(registerModel))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] LoginModel loginModel)
        {
            if (await _service.LoginAsync(loginModel))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ObligatoriskOpgave.Models;
using ObligatoriskOpgave.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObligatoriskOpgave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private AuthRepository _authRepository;
        public AuthController(IConfiguration config, AuthRepository authRepository)
        {
            _config = config;
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Login([FromBody] LoginRequest login)
        {
            // 1. Validate the user (In a real scenario, check your database here)
            // Here we use a simple hardcoded check:

            var result = _authRepository.Login(login);
            if (result == null)
            {
                return Unauthorized("Invalid username or password.");

            }
            else
            {
                return Ok(result);
            }
        }
    }
}


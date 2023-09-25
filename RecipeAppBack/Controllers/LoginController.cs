using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBack.Dto;
using RecipeAppBack.Services.Interfaces;

namespace RecipeAppBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] RegisterDto registerDto)
        {
            var token = _loginService.Login(registerDto);
            if (token != null)
            {
                if (token == string.Empty)
                {
                    return BadRequest("Wrong password!");

                }
                return Ok(token);
            }
            else
            {
                return BadRequest("Wrong username!");
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                _loginService.Register(registerDto);
                return NoContent(); 
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

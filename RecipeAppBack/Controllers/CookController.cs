using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBack.Dto;
using RecipeAppBack.Services.Interfaces;

namespace RecipeAppBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookController : ControllerBase
    {
        private readonly ICookService _cookService;
        public CookController(ICookService cookService)
        {
            _cookService = cookService;
        }

        [HttpGet("getAllCooks")]
        [Authorize(Roles ="Admin")]
        public IActionResult GettAllCooks()
        {
            
            return Ok(_cookService.GetAllCooks());
        }

        [HttpPost("addCook")]
        [Authorize(Roles ="Admin")]
        public IActionResult AddCook([FromBody] RegisterDto cookDto)
        {
            _cookService.AddCook(cookDto);

            return Ok("Cook added.");
        }
    }
}

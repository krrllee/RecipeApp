using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAppBack.Dto;
using RecipeAppBack.Services.Interfaces;

namespace RecipeAppBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IUserSevice _userSevice;
        public RecipeController(IRecipeService recipeService, IUserSevice userSevice)
        {
            _recipeService = recipeService;
            _userSevice = userSevice;
        }

        [HttpPost("AddRecipe")]
        [Authorize(Roles = "Cook")]
        public IActionResult AddRecipe([FromBody] RecipeWithIngredientsDto recipeWithIngredientsDto)
        {
            try
            {
                var username = User.Identity.Name;
                _recipeService.AddRecipe(recipeWithIngredientsDto, username);
                return Ok("Recipe added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteCooksRecipe")]
        [Authorize(Roles = "Cook")]
        public IActionResult DeleteCooksRecipe(int id)
        {

            try
            {
                var username = User.Identity.Name;
                _recipeService.DeleteCooksRecipe(id, username);
                return Ok("Recipe deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleRecipe")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                _recipeService.DeleteRecipe(id);
                return Ok("Recipe deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetIngredients")]
        [Authorize(Roles = "Admin,Cook,User")]
        public IActionResult Ingredients(int id)
        {
            try
            {
                return Ok(_recipeService.GetIngredients(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("AddIngredients")]
        [Authorize(Roles ="Admin")]
        public IActionResult AddIngredients(int recipeId, [FromBody] List<IngredientDto> newIngredients)
        {
            try
            {
                _recipeService.AddIngredientsToRecipe(recipeId, newIngredients);
                return Ok("Ingredients added.");
            }
           
            catch (Exception ex)
            {
                return BadRequest($"Greška: {ex.Message}");
            }
        }

        [HttpPost("DeleteIngredient")]
        [Authorize(Roles ="Admin")]
        public IActionResult DeleteIngredient(int ingredientId)
        {
            try
            {
                _recipeService.DeleteIngredient(ingredientId);
                return Ok("Ingredient deleted.");
            }

            catch (Exception ex)
            {
                return BadRequest($"Greška: {ex.Message}");
            }
        }

        [HttpGet("GetAllRecipes")]
        [Authorize(Roles ="Admin,Cook,User")]
        public IActionResult GetAllRecipes()
        {
            try
            {
                return Ok(_recipeService.GetAllRecipes());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddToBookmark")]
        [Authorize(Roles ="User")]
        public IActionResult AddToBookmark(int id)
        {
            try
            {
                var username = User.Identity.Name;
                _recipeService.AddToBookmark(id, username);
                return Ok("Recipe is added to bookmark.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveFromBookmark")]
        [Authorize(Roles = "User")]
        public IActionResult RemoveFromBookmark(int id)
        {
            try
            {
                _recipeService.RemoveFromBookmark(id);
                return Ok("Recipe is removed from bookmark.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRecipesFromBookmark")]
        [Authorize(Roles ="User")]
        public IActionResult GetRecipesFromBookmark()
        {
            try
            {
                var username = User.Identity.Name;
                return Ok(_recipeService.GetRecipesFromBookmark(username));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBookmarks")]
        [Authorize(Roles ="User")]
        public IActionResult GetBookmarks()
        {
            try
            {
                var username = User.Identity.Name;
                return Ok(_recipeService.GetBookmarks(username));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

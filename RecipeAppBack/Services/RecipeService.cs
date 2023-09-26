using RecipeAppBack.Dto;
using RecipeAppBack.Models;
using RecipeAppBack.Repositories.Interfaces;
using RecipeAppBack.Services.Interfaces;

namespace RecipeAppBack.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        public RecipeService(IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        public void AddIngredientsToRecipe(int recipeId, List<IngredientDto> ingredients)
        {
            var recipe = _recipeRepository.GetRecipeForAdmin(recipeId);
            if (recipe != null)
            {
                foreach (var ingredientDto in ingredients)
                {
                    recipe.ingredients.Add(new Ingredient
                    {
                        Name = ingredientDto.Name,
                        Quantity = ingredientDto.Quantity
                    });
                }

                _recipeRepository.UpdateRecipe(recipe);
            }
            else
            {
                throw new Exception("Recept nije pronađen."); 
            }
        }

        public void AddRecipe(RecipeWithIngredientsDto recipeWithIngredientsDto, string username)
        {
            var user = _userRepository.getByUsername(username);
            var recipe = new Recipe
            {
                Name = recipeWithIngredientsDto.Name,
                Description = recipeWithIngredientsDto.Description,
                AuthorId = user.Id,
                CreatedAt = DateTime.Now
            
            };

            foreach(var ingredientDto in recipeWithIngredientsDto.Ingredients)
            {
                recipe.ingredients.Add(new Ingredient
                {
                    Name = ingredientDto.Name,
                    Quantity = ingredientDto.Quantity,
                });
                
            }
            _recipeRepository.AddRecipe(recipe);
        }

        public void AddToBookmark(int id, string username)
        {
            var user = _userRepository.getByUsername(username);
            var recipe = _recipeRepository.GetRecipeForAdmin(id);

            var bookmark = new Bookmark {
                UserId = user.Id,
                RecipeId = recipe.Id,
            };
            _recipeRepository.AddToBookmark(bookmark);
        }

        public void DeleteCooksRecipe(int id, string username)
        {
           var user = _userRepository.getByUsername(username);
           var recipe =  _recipeRepository.GetRecipeForCook(id, user.Id);

            _recipeRepository.DeleteCooksRecipe(recipe);
        }

        public void DeleteIngredient(int id)
        {
            var ingredient = _recipeRepository.GetIngredient(id);
            _recipeRepository.DeleteIngredient(ingredient);
        }

        public void DeleteRecipe(int id)
        {
            var recipe = _recipeRepository.GetRecipeForAdmin(id);
            _recipeRepository.DeleteRecipe(recipe);
        }

        public List<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAllRecipes();
        }

        public List<Bookmark> GetBookmarks(string username)
        {
            var user = _userRepository.getByUsername(username);
           return _recipeRepository.GetBookmarks(user.Id);
        }

        public List<Ingredient> GetIngredients(int id)
        {
            var ingredients = _recipeRepository.GetIngredientsById(id);
            return ingredients;
        }

        public List<Recipe> GetRecipesFromBookmark(string username)
        {
            var user = _userRepository.getByUsername(username);
            return _recipeRepository.GetRecipesFromBookmark(user.Id);
        }

        public void RemoveFromBookmark(int id)
        {
            var bookmark = _recipeRepository.GetFromBookmark(id);
            _recipeRepository.RemoveFromBookmark(bookmark);
        }

        public IEnumerable<Recipe> SearchRecipes(string searchTerm)
        {
            return _recipeRepository.SearchRecipes(searchTerm);
        }
    }
}

using RecipeAppBack.Dto;
using RecipeAppBack.Models;

namespace RecipeAppBack.Services.Interfaces
{
    public interface IRecipeService
    {
        void AddRecipe(RecipeWithIngredientsDto recipeWithIngredientsDto,string username);
        void DeleteCooksRecipe(int id,string username);
        void DeleteRecipe(int id);
        List<Ingredient> GetIngredients(int id);
        void AddIngredientsToRecipe(int recipeId,List<IngredientDto> ingredients);
        void DeleteIngredient(int id);
        List<Recipe> GetAllRecipes();
        void AddToBookmark(int id,string username);
        void RemoveFromBookmark(int id);
        List<Recipe> GetRecipesFromBookmark(string username);
        List<Bookmark> GetBookmarks(string username);
        IEnumerable<Recipe> SearchRecipes(string searchTerm);
    }
}

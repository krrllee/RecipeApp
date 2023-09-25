using RecipeAppBack.Models;

namespace RecipeAppBack.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        void AddRecipe(Recipe recipe);
        Recipe GetRecipeForCook(int id,int userId);
        Recipe GetRecipeForAdmin(int id);
        void DeleteCooksRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
        List<Ingredient> GetIngredientsById(int id);
        Ingredient GetIngredient(int id);
        void UpdateRecipe(Recipe recipe);
        void DeleteIngredient(Ingredient ingredient);
        List<Recipe> GetAllRecipes();
        void AddToBookmark(Bookmark bookmark);
        Bookmark GetFromBookmark(int id);
        void RemoveFromBookmark(Bookmark bookmark);
        List<Recipe> GetRecipesFromBookmark(int id);
        List<Bookmark> GetBookmarks(int id);
    }
}

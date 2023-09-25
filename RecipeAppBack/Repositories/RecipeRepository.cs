using RecipeAppBack.Models;
using RecipeAppBack.Repositories.Interfaces;

namespace RecipeAppBack.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;
        public RecipeRepository(AppDbContext context)
        {
                _context = context;
        }
        public void AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void AddToBookmark(Bookmark bookmark)
        {
            _context.Bookmarks.Add(bookmark);
            _context.SaveChanges();

        }

        public void DeleteCooksRecipe(Recipe recipe)
        {
            var ingredients = _context.Ingredients.Where(x => x.RecipeId == recipe.Id).ToList();
            foreach(var i  in ingredients)
            {
                _context.Ingredients.Remove(i); 
                _context.SaveChanges();
            }
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient); 
            _context.SaveChanges();
        }

        public void DeleteRecipe(Recipe recipe)
        {
            var ingredients = _context.Ingredients.Where(x => x.RecipeId == recipe.Id).ToList();
            foreach (var i in ingredients)
            {
                _context.Ingredients.Remove(i);
                _context.SaveChanges();
            }
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
        }

        public List<Recipe> GetAllRecipes()
        {
            return _context.Recipes.ToList();
        }

        public List<Bookmark> GetBookmarks(int id)
        {
           return _context.Bookmarks.Where(b=>b.UserId == id).ToList();
        }

        public Bookmark GetFromBookmark(int id)
        {
            return _context.Bookmarks.Where(i => i.Id == id).FirstOrDefault();
        }

        public Ingredient GetIngredient(int id)
        {
            return _context.Ingredients.FirstOrDefault(x => x.Id == id);
        }

        public List<Ingredient> GetIngredientsById(int id)
        {
            return _context.Ingredients.Where(i=>i.RecipeId == id).ToList();
        }

        public Recipe GetRecipeForAdmin(int id)
        {
            return _context.Recipes.Where(r => r.Id == id).FirstOrDefault();
        }

        public Recipe GetRecipeForCook(int id, int userId)
        {
            return _context.Recipes.Where(r => r.Id == id && r.AuthorId == userId).FirstOrDefault();
        }

        public List<Recipe> GetRecipesFromBookmark(int id)
        {
            var bookmarkedRecipeIds = _context.Bookmarks.Where(b=>b.UserId == id).Select(b=>b.RecipeId).ToList();
            var bookmarkedRecipes = _context.Recipes.Where(r=>bookmarkedRecipeIds.Contains(r.Id)).ToList();
            return bookmarkedRecipes;
        }

        public void RemoveFromBookmark(Bookmark bookmark)
        {
            _context.Bookmarks.Remove(bookmark);
            _context.SaveChanges();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            _context.SaveChanges();
        }
    }
}

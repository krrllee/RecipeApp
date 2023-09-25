using RecipeAppBack.Models;

namespace RecipeAppBack.Repositories.Interfaces
{
    public interface ICookRepository
    {
        List<User> getCooks();
        void AddCook(User user);
    }
}

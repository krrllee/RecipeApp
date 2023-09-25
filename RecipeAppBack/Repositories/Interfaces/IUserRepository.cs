using RecipeAppBack.Models;

namespace RecipeAppBack.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User getByUsername(string username);
    }
}

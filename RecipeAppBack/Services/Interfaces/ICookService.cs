using RecipeAppBack.Dto;
using RecipeAppBack.Models;

namespace RecipeAppBack.Services.Interfaces
{
    public interface ICookService
    {
        List<User> GetAllCooks();
        void AddCook(RegisterDto cookDto);
    }
}

using RecipeAppBack.Dto;
using RecipeAppBack.Models;
using RecipeAppBack.Repositories;
using RecipeAppBack.Repositories.Interfaces;
using RecipeAppBack.Services.Interfaces;

namespace RecipeAppBack.Services
{
    public class CookService : ICookService
    {
        public readonly ICookRepository _cookRepository;
        public CookService(ICookRepository cookRepository)
        {
            _cookRepository = cookRepository;
        }

        public void AddCook(RegisterDto cookDto)
        {
            try
            {
                var newUser = new User
                {
                    UserName = cookDto.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(cookDto.Password),
                    Role = "Cook"
                };
                _cookRepository.AddCook(newUser);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<User> GetAllCooks()
        {
            var cooks = _cookRepository.getCooks();
            return cooks;
        }
    }
}

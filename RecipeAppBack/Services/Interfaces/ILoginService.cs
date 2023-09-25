using RecipeAppBack.Dto;

namespace RecipeAppBack.Services.Interfaces
{
    public interface ILoginService
    {
        void Register(RegisterDto registerDto);
        string Login(RegisterDto registerDto);
    }
}

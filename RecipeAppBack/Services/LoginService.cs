using Microsoft.IdentityModel.Tokens;
using RecipeAppBack.Dto;
using RecipeAppBack.Models;
using RecipeAppBack.Repositories.Interfaces;
using RecipeAppBack.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeAppBack.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfigurationSection _secretKey;


        public LoginService(ILoginRepository loginRepository, IConfiguration config,IUserRepository userRepository) { 
            _loginRepository = loginRepository;
            _secretKey = config.GetSection("SecretKey");
            _userRepository = userRepository;
        }

        public string Login(RegisterDto registerDto)
        {
            var user = _userRepository.getByUsername(registerDto.UserName);
            if (user == null)
            {
                return ("User not found.");
            }
            if (!BCrypt.Net.BCrypt.Verify(registerDto.Password, user.Password))
            {
                return ("Invalid password.");
            }

            List<Claim>claims = new List<Claim>();  
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Role, user.Role));

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:44398",
                claims: claims, //claimovi
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public void Register(RegisterDto registerDto)
        {
            try
            {
                var newUser = new User
                {
                    UserName = registerDto.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                    Role = "User"
                };
                _loginRepository.Register(newUser);

           
            }catch (Exception ex)
            {
                Console.WriteLine( ex.Message);
            }
        }
    }
}

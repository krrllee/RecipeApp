using RecipeAppBack.Models;
using RecipeAppBack.Repositories.Interfaces;

namespace RecipeAppBack.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public User getByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}

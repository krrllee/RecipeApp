using RecipeAppBack.Models;
using RecipeAppBack.Repositories.Interfaces;

namespace RecipeAppBack.Repositories
{
    public class CookRepository : ICookRepository
    {
        private readonly AppDbContext _context;
        public CookRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCook(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> getCooks()
        {
            return _context.Users.Where(u => u.Role == "Cook").ToList();
        }
    }
}

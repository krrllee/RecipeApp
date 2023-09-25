using Microsoft.EntityFrameworkCore;

namespace RecipeAppBack.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cook> Cooks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.ingredients) // Recept ima mnogo sastojaka
                .WithOne() // Svaki sastojak pripada samo jednom receptu
                .HasForeignKey(ing => ing.RecipeId); // Spoljni ključ u sastojku koji pokazuje na recept
        }


    }



}

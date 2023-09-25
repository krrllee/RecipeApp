namespace RecipeAppBack.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RecipeId { get; set; }
    }
}

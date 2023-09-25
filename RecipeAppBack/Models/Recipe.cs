namespace RecipeAppBack.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Ingredient>? ingredients { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt {  get; set; }

        public Recipe()
        {
            ingredients = new List<Ingredient>();
        }
    }
}

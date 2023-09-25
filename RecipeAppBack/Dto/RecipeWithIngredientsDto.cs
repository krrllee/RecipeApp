namespace RecipeAppBack.Dto
{
    public class RecipeWithIngredientsDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<IngredientDto>? Ingredients { get; set; }
    }
}

using Microsoft.OpenApi.Models;

namespace RecipeAppBack.Models
{
    public class Cook
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Bio { get; set; }
    }
}

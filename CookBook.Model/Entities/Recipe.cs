using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Model.Entities
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string Title { get; set; }

        public int CookbookId { get; set; }
        [ForeignKey("CookbookId")]
        public Cookbook Cookbook { get; set; }
    }
}
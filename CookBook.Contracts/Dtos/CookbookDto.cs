using System.Collections.Generic;

namespace CookBook.Contracts.Dtos
{
    public class CookbookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RecipeDto> Recipes { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Model.Entities
{
    public class Cookbook
    {
        [Key]
        public int CookbookId { get; set ;}
        public string Name { get; set; }
        
        public List<Recipe> Recipes { get; set; }
    }
}
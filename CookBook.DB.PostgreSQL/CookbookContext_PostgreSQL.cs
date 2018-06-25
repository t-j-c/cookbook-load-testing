using CookBook.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookBook.DB.PostgreSQL
{
    public class CookbookContext_PostgreSQL : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Cookbook> Cookbooks { get; set; }

        public CookbookContext_PostgreSQL(DbContextOptions<CookbookContext_PostgreSQL> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Recipe>().HasKey(e => e.RecipeId);
            builder.Entity<Cookbook>().HasKey(e => e.CookbookId);

            base.OnModelCreating(builder);
        }
    }
}

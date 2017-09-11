using System;
using System.IO;
using CookBook.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CookBook.Model
{
    public partial class CookbookContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Cookbook> Cookbooks { get; set; }

        public CookbookContext(DbContextOptions<CookbookContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Recipe>().HasKey(e => e.RecipeId);
            builder.Entity<Cookbook>().HasKey(e => e.CookbookId);

            base.OnModelCreating(builder);
        }
    }
}

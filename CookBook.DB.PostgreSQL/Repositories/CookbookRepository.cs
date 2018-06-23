using System.Collections.Generic;
using System.Linq;
using CookBook.Model.Entities;
using CookBook.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.DB.PostgreSQL.Repositories
{
    public class CookbookRepository : ICookbookRepository
    {
        private readonly CookbookContext_PostgreSQL _dbContext;
        public CookbookRepository(CookbookContext_PostgreSQL dbContext)
        {
            _dbContext = dbContext;
        }

        public int Count()
        {
            return _dbContext.Cookbooks.Count();
        }

        public ICollection<Cookbook> Get()
        {
            return _dbContext.Cookbooks.AsNoTracking().ToList();
        }

        public Cookbook Get(string name)
        {
            return _dbContext.Cookbooks.SingleOrDefault(c => c.Name == name);
        }

        public void Add(string name)
        {
            _dbContext.Cookbooks.Add(new Cookbook
            {
                Name = name,
                Recipes = new List<Recipe>()
            });
            _dbContext.SaveChanges();
        }

        public void Delete()
        {
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE \"Cookbooks\" CASCADE;");
        }
    }
}

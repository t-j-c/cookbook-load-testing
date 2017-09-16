using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CookBook.DB.PostgreSQL;

namespace CookBook.API.Controllers
{
    [Route("api/[controller]")]
    public class CookbookController : Controller
    {
        private readonly CookbookContext_PostgreSQL _db;
        public CookbookController(CookbookContext_PostgreSQL db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] 
            { 
                $"CookBooks: {_db.Cookbooks.Count()}"
            };
        }
    }
}

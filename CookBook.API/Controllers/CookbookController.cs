using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CookBook.Model.Repositories;

namespace CookBook.API.Controllers
{
    [Route("api/[controller]")]
    public class CookbookController : Controller
    {
        private readonly ICookbookRepository _db;
        public CookbookController(ICookbookRepository db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] 
            { 
                $"CookBooks: {_db.Count()}"
            };
        }
    }
}

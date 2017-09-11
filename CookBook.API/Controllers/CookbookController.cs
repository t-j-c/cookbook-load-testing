using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CookBook.Model;

namespace CookBook.API.Controllers
{
    [Route("api/[controller]")]
    public class CookbookController : Controller
    {
        private readonly CookbookContext _db;
        public CookbookController(CookbookContext db)
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

        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}

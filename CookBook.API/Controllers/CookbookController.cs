using Microsoft.AspNetCore.Mvc;
using CookBook.Model.Repositories;
using CookBook.Contracts.Messages;
using CookBook.Contracts.Dtos;
using System.Linq;
using CookBook.Model.Entities;
using System.Collections.Generic;

namespace CookBook.API.Controllers
{
    [Route("api/cookbook")]
    public class CookbookController : Controller
    {
        private readonly ICookbookRepository _cookbookRepository;
        public CookbookController(ICookbookRepository cookbookRepository)
        {
            _cookbookRepository = cookbookRepository;
        }
        
        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(200, Type = typeof(CookbookDto))]
        [ProducesResponseType(404)]
        public IActionResult Get(string name)
        {
            var result = _cookbookRepository.Get(name);
            if (result == null) return NotFound();

            return Ok(Map(result));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Add([FromBody] AddCookbookCommand message)
        {
            _cookbookRepository.Add(message.Name);

            return Ok();
        }

        [HttpDelete]
        [Route("all")]
        [ProducesResponseType(200)]
        public IActionResult Delete()
        {
            _cookbookRepository.Delete();

            return Ok();
        }

        private static CookbookDto Map(Cookbook model)
        {
            return new CookbookDto
            {
                Id = model.CookbookId,
                Name = model.Name,
                Recipes = model.Recipes?.Select(Map).ToList() ?? new List<RecipeDto>()
            };
        }

        private static RecipeDto Map(Recipe model)
        {
            return new RecipeDto
            {
                Id = model.RecipeId,
                Name = model.Title,
                CookbookId = model.CookbookId
            };
        }
    }
}

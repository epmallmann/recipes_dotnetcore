using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        [HttpGet]
        public JsonResult Get()
        {
            using (var db = new RecipeContext())
            {
                var result = db.Recipes.AsNoTracking().ToList();
                return new JsonResult(result);
            }
        }

        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            using (var db = new RecipeContext())
            {
                var model = db.Recipes.AsNoTracking().FirstOrDefault(x => x.Id == id);
                return new JsonResult(model);
            }
        }

        [HttpPost]
        public void Post([FromBody] Recipe recipe)
        {
            using (var db = new RecipeContext())
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
            }
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Recipe recipe)
        {
            using (var db = new RecipeContext())
            {
                var model = db.Recipes.FirstOrDefault(x => x.Id == id);
                model.Name = recipe.Name;
                model.Title = recipe.Title;
                db.Recipes.Update(recipe);
                db.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes a recipe",
            Description = "Deletes a recipe of a given id",
            OperationId = "Delete"
            )]
        [ProducesResponseType(typeof(OkResult), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete([SwaggerParameter("the guid id", Required = true)]Guid id)
        {
            using (var db = new RecipeContext())
            {
                var model = db.Recipes.FirstOrDefault(x => x.Id == id);
                if (model == null)
                    return new NotFoundResult();

                db.Recipes.Remove(model);
                db.SaveChanges();

                return Ok();
            }
        }
    }
}

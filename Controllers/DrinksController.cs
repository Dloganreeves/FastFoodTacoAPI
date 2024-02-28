using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]

        public IActionResult GetAllDrinks(string? SortByCost)
        {
            List<Drink> result = dbContext.Drinks.ToList();
            if (SortByCost == "ascending")
            {
                result = result.OrderBy(x => x.Cost).ToList();
            }
            else if (SortByCost == "descending")
            {
                result = result.OrderByDescending(x => x.Cost).ToList();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetDrinkssById(int id)
        {
            Drink result = dbContext.Drinks.Find(id);
            if (result == null)
            {
                return NotFound("Taco not found");
            }
            return Ok(result);
        }
        [HttpPost()] 

        public IActionResult AddDrink([FromBody] Drink newdrink)
        {
            newdrink.Id = 0;
            dbContext.Drinks.Add(newdrink);
            dbContext.SaveChanges();
            return Created($"/api/Drinks/{newdrink.Id}", newdrink);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateDrink([FromBody] Drink targetDrink, int id)
        {
            if (id != targetDrink.Id)
            {
                return BadRequest();
            }
            if (!dbContext.Drinks.Any(d => d.Id == id))
            {
                return NotFound();
            }

            dbContext.Drinks.Update(targetDrink);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
    }
}

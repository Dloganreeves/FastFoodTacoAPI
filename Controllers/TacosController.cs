using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        private FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]

        public IActionResult GetAllTaco(bool? Softshell = null ) 
        {
            List<Taco> result = dbContext.Tacos.ToList();
            if ( Softshell != null )
            {
                result = result.Where(s => s.SoftShell == Softshell).ToList();
            }
            return Ok( result );
        }

        [HttpGet("{id}")]
        public IActionResult GetTacosById(int id)
        {
            Taco result = dbContext.Tacos.Find(id);
            if (result == null)
            {
                return NotFound("Taco not found");
            }
            return Ok(result);
        }

        [HttpPost()]

        public IActionResult AddTaco([FromBody] Taco newTaco)
        {
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();

            return Created($"api/Tacos/{newTaco.Id}", newTaco);
        }

        [HttpDelete()]

        public IActionResult DeleteTaco(int id) 
        {
            Taco result = dbContext.Tacos.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            dbContext.Tacos.Remove(result);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}

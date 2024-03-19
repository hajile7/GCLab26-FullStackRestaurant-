using GCLab26_FullStackRestaurant_backend_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GCLab26_FullStackRestaurant_backend_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private RestaurantDbContext dbContext = new RestaurantDbContext();

        [HttpGet]
        public IActionResult GetAll(string? filter, bool? again)
        {
            if (filter != null && again == null)
            {
                List<Order> result = dbContext.Orders.Where(s => s.Restaurant.ToLower() == filter.ToLower()).ToList();
                return Ok(result);
            }
            else if (filter == null && again != null)
            {
                List<Order> result = dbContext.Orders.Where(s => s.OrderAgain == again).ToList();
                return Ok(result);
            }
            else if (filter != null && again != null)
            {
                List<Order> result = dbContext.Orders.Where(s => s.Restaurant.ToLower() == filter.ToLower() && s.OrderAgain == again).ToList();
                return Ok(result);
            }
            else
            {
                List<Order> result = dbContext.Orders.ToList();
                return Ok(result);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Order result = dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        public IActionResult Add([FromBody] Order o)
        {
            o.Id = 0;
            dbContext.Orders.Add(o);
            dbContext.SaveChanges();
            return CreatedAtAction("Get", new
            {
                id = o.Id
            }, o);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Order o, int id)
        {
            if (o.Id != id)
            {
                return BadRequest();
            }
            else if (!dbContext.Orders.Any(o => o.Id == id)) {
                return NotFound();
            }
            else
            {
                dbContext.Orders.Update(o);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Order result = dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            dbContext.Orders.Remove(result);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}

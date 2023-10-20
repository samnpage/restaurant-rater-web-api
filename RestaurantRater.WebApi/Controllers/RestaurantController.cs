using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantRater.WebApi.Data;

namespace RestaurantRater.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
// This is where we will create API endpoints(CRUD methods) to access our data
public class RestaurantController : ControllerBase
{
    // Constructor that injects RestaurantDbContext
    private readonly RestaurantDbContext _context;
    public RestaurantController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    // Async GET Endpoint (Method that returns the list of Restaurant objects from the database as an OK (200) response)
    public async Task<IActionResult> GetRestaurants()
    {
        List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    // Async GET endpoint (Retrieves a specific restaurant using the primary key, or id.)
    public async Task<IActionResult> GetRestaurantbyId(int id)
    {
        // Fetches restaurant data from the database
        Restaurant? restaurant = await _context.Restaurants.FindAsync(id);

        if (restaurant is null) // if null 
        {
            return NotFound(); // it returns a Not Found error (404)
        }
        return Ok(restaurant); // otherwise, it returns the restaurant
    }

    // Async POST Endpoint
    [HttpPost]
    public async Task<IActionResult> PostRestaurant([FromBody] Restaurant request)
    {
        if (ModelState.IsValid)
        {
            _context.Restaurants.Add(request); // If response is valid it will return a Ok (200) response
            await _context.SaveChangesAsync();
            return Ok();
        }

        return BadRequest(ModelState); // Otherwise it will return a Bad Reques (400) response
    }
}



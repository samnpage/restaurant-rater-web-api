using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantRater.WebApi.Data;
using RestaurantRater.WebApi.Models;

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
    public async Task<IActionResult> GetAllRestaurantsAsync()
    {
        List<Restaurant> restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();

        List<RestaurantListItem> restaurantList = restaurants.Select(r => new RestaurantListItem() {
            Id = r.Id,
            Name = r.Name,
            Location = r.Location,
            AverageRating = r.AverageRating,
        }).ToList();

        return Ok(restaurantList);
    }

    [HttpGet("{id:int}")] // https://localhost:7017/Restaurant/1
    // Async GET endpoint (Retrieves a specific restaurant using the primary key, or id.)
    public async Task<IActionResult> GetRestaurantbyIdAsync(int id)
    {
        // Fetches restaurant data from the database
        Restaurant? restaurant = await _context.Restaurants.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == id);

        if (restaurant is null) // if null 
            return NotFound(); // it returns a Not Found error (404)

        return Ok(restaurant); // otherwise, it returns the restaurant
    }

    // Async POST Endpoint
    [HttpPost]
    public async Task<IActionResult> PostRestaurant([FromBody] RestaurantCreate request) // FromBody = JSON
    {
        if (ModelState.IsValid)
        {
            Restaurant restaurantToAdd = new()
            {
                Name = request.Name,
                Location = request.Location
            };

            _context.Restaurants.Add(restaurantToAdd); // If response is valid it will return a Ok (200) response
            await _context.SaveChangesAsync();
            return Ok();
        }

        return BadRequest(ModelState); // Otherwise it will return a Bad Reques (400) response
    }

    // // Async PUT Endpoint
    // [HttpPut]
    // public async Task<IActionResult> PutRestaurant([FromBody] RestaurantEdit request)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }

    //     Restaurant? restaurant = await _context.Restaurants.FindAsync(request.Id);
    //     if (restaurant is null)
    //     {
    //         return NotFound();
    //     }

    //     restaurant.Name = request.Name;
    //     restaurant.Location = request.Location;

    //     _context.Restaurants.Update(restaurant);
    //     await _context.SaveChangesAsync();
    //     return Ok();
    // }

    // Async Put Method
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutRestaurant([FromForm] RestaurantEdit model, [FromRoute] int id)
    {
        // Retrieves requested Restaurant using Id parameter
        var oldRestaurant = await _context.Restaurants.FindAsync(id);

        if (oldRestaurant == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!string.IsNullOrEmpty(model.Name))
        {
            oldRestaurant.Name = model.Name;
        }
        if (!string.IsNullOrEmpty(model.Location))
        {
            oldRestaurant.Location = model.Location;
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    // Async DELETE Method
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);

        if (restaurant == null)
        {
            return NotFound();
        }

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
        return Ok();
    }
}



using Microsoft.EntityFrameworkCore;

namespace RestaurantRater.WebApi;

public class RestaurantDbContext : DbContext
{
    // Constructor that inherits from the base (DbContext) controller
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }
}

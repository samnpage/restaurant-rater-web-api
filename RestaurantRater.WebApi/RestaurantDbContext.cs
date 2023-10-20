using Microsoft.EntityFrameworkCore;
using RestaurantRater.WebApi.Data;

namespace RestaurantRater.WebApi;

public class RestaurantDbContext : DbContext
{
    // Constructor that inherits from the base (DbContext) controller
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }

    // DbSets to represent our tables
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Rating> Ratings { get; set; }
}

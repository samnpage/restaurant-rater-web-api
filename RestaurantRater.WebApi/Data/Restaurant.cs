using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace RestaurantRater.WebApi.Data;

// ! Restaurant Entity
public class Restaurant
{
    [Key] // Primary Key
    public int Id { get; set; }

    [Required] // NOT NULL
    [MaxLength(100)] // NVARCHAR(100)
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(100)] // Attributes can go in the same brackets.
    public string Location { get; set; } = string.Empty;

    public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

    public float AverageRating
    {
        get
        {
            if (Ratings.Count == 0)
            {
                return 0;
            }
            float total = 0.0f;
            foreach (Rating rating in Ratings)
            {
                total += rating.Score;
            }
            return total / Ratings.Count;
        }
    }
}

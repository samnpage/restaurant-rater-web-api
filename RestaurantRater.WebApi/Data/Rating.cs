using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRater.WebApi.Data;

public class Rating
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(0,5)]
    public int Score { get; set; }

    [Required]
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
}
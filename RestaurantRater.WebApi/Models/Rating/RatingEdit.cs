using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRater.WebApi.Models
{
    public class RatingEdit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 5)]
        public float Score { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
    }
}
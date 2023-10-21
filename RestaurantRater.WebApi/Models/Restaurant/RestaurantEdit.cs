using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRater.WebApi.Data;


namespace RestaurantRater.WebApi.Models
{
    public class RestaurantEdit
    {
        [Required] // NOT NULL
        [MaxLength(100)] // NVARCHAR(100)
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)] // Attributes can go in the same brackets.
        public string Location { get; set; } = string.Empty;
    }
}
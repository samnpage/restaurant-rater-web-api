using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRater.WebApi.Models
{
    public class RestaurantRequest
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
    }
}
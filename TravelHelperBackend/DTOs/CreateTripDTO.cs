using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class CreateTripDTO
    {
        [Required]
        public int TripDestanation { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
    }
}

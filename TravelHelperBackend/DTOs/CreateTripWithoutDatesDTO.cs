using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class CreateTripWithoutDatesDTO
    {
        [Required]
        public int TripDestanation { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

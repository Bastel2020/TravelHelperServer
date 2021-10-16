using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class CreateTripWithoutDatesDTO
    {
        public int TripDestanation { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

using System.Collections.Generic;

namespace TravelHelperBackend.Database.Models
{
    public class PlaceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Place> Places { get; set; }
    }
}
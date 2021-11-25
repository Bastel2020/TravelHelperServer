using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.Database.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Trip> PlannedTrips { get; set; }
        public List<FileModel> Photos { get; set; }
        public FileModel MainPhoto { get; set; }
        public List<Place> Places { get; set; }
    }
}

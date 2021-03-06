using System.Collections.Generic;
using TravelHelperBackend.Enums;

namespace TravelHelperBackend.Database.Models
{
    public class Place
    {
        public int Id { get; set; }
        public City City { get; set; }
        public PlaceCategory PlaceCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PlaceSite { get; set; }
        public string EnterCost { get; set; }
        public FileModel MainPhoto { get; set; }
        public List<FileModel> Photos { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Location { get; set; }
        public string TextLocation { get; set; }
        public string WorkingHours { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;

namespace TravelHelperBackend.DTOs
{
    public class PlaceInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainPhotoUrl { get; set; }
        public List<string> PhotosUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Location { get; set; }
        public string TextLocation { get; set; }
        public string WorkingHours { get; set; }

        public PlaceInfoDTO() { }

        public PlaceInfoDTO(Place placeToParse)
        {
            Id = placeToParse.Id;
            Name = placeToParse.Name;
            Description = placeToParse.Description;
            if (placeToParse.MainPhoto != null && placeToParse.MainPhoto.Id != 0)
                MainPhotoUrl = $"/files/photos/{placeToParse.MainPhoto.Id}";
            if (placeToParse.Photos != null)
                PhotosUrl = placeToParse.Photos
                    .Where(p => p.Id != 0)
                    .Select(p => $"/files/photos/{p.Id}").ToList();
            Latitude = placeToParse.Latitude;
            Longitude = placeToParse.Longitude;
            Location = placeToParse.Location;
            TextLocation = placeToParse.TextLocation;
            WorkingHours = placeToParse.WorkingHours;
        }
    }
}

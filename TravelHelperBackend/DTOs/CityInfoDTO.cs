using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;

namespace TravelHelperBackend.DTOs
{
    public class CityInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainPhotoUrl { get; set; }
        public List<string> PhotosUrls { get; set; }

        public CityInfoDTO() { }

        public CityInfoDTO(City cityToParse)
        {
            Id = cityToParse.Id;
            Name = cityToParse.Name;
            Description = cityToParse.Description;
            if (cityToParse.MainPhoto != null && cityToParse.MainPhoto.Id != 0)
                MainPhotoUrl = $"http://188.186.7.171/TravelHelperBackend/api/files/photos/{cityToParse.MainPhoto.Id}";
            if (cityToParse.Photos != null)
                PhotosUrls = cityToParse.Photos
                    .Where(p => p.Id != 0)
                    .Select(p => $"http://188.186.7.171/TravelHelperBackend/api/files/photos/{p.Id}").ToList();
        }
    }
}

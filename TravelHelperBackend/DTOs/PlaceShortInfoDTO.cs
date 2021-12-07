using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;
using TravelHelperBackend.Enums;

namespace TravelHelperBackend.DTOs
{
    public class PlaceShortInfoDTO
    {
        public int Id { get; set; }
        [field:NonSerialized]
        public PlaceCategory PlaceCategory { get; set; }
        public string Name { get; set; }
        public string TextLocation { get; set; }
        public string MainPhotoUrl { get; set; }

        public PlaceShortInfoDTO(Place placeToParse)
        {
            Id = placeToParse.Id;
            PlaceCategory = placeToParse.PlaceCategory;
            Name = placeToParse.Name;
            TextLocation = placeToParse.TextLocation;

            if (placeToParse.MainPhoto != null && placeToParse.MainPhoto.Id != 0)
                MainPhotoUrl = $"/files/photos/{placeToParse.MainPhoto.Id}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;

namespace TravelHelperBackend.DTOs
{
    public class PlaceCategoryShortDTO
    {
        public string Category { get; set; }
        public List<PlaceShortInfoDTO> Places { get; set; }

        public PlaceCategoryShortDTO() { }

        public PlaceCategoryShortDTO(IEnumerable<Place> placesToParse)
        {
            if (placesToParse.Count() == 0)
                return;

            var places = placesToParse.ToArray();
            if (!placesToParse.All(p => p.PlaceCategory == placesToParse.First().PlaceCategory))
                throw new ArgumentException("Все места должны быть из одной категории!");

            Category = placesToParse.First().PlaceCategory.ToString();

            Places = placesToParse
                .Select(p => new PlaceShortInfoDTO(p))
                .ToList();
        }
    }
}

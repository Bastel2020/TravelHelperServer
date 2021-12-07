using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;
using TravelHelperBackend.Enums;

namespace TravelHelperBackend.DTOs
{
    public class PlaceCategoryDTO
    {
        public string Category { get; set; }
        public List<PlaceInfoDTO> Places { get; set; }

        public PlaceCategoryDTO() { }

        public PlaceCategoryDTO(IEnumerable<Place> placesToParse)
        {
            if (placesToParse.Count() == 0)
                return;

            var places = placesToParse.ToArray();
            if (!placesToParse.All(p => p.PlaceCategory == placesToParse.First().PlaceCategory))
                throw new ArgumentException("Все места должны быть из одной категории!");

            Category = placesToParse.First().PlaceCategory.ToString();

            Places = placesToParse
                .Select(p => new PlaceInfoDTO(p))
                .ToList();
        }
    }
}

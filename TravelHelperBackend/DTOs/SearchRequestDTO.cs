using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Enums;

namespace TravelHelperBackend.DTOs
{
    public class SearchRequestDTO
    {
        [Required]
        public string Name { get; set; }
        public int CityId { get; set; }
        public int placeCategory { get; set; }

    }
}

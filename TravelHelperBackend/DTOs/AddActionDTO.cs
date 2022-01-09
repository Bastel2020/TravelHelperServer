using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravelHelperBackend.DTOs
{
    public class AddActionDTO
    {
        [Required]
        public long TripDayId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public object[] Files { get; set; }
        [Required]
        public string TimeOfAction{ get; set; }

        public AddActionDTO()
        {
            //TimeOfAction = "-1";
        }
    }
}

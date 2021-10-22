using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class AddTripDayDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime TimeToAdd { get; set; }
    }
}

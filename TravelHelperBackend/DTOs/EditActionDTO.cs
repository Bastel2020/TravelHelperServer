using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class EditActionDTO
    {
        [Required]
        public long ActionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public object[] Files { get; set; }
        [Required]
        public string TimeOfAction { get; set; }
        public EditActionDTO()
        {
            //TimeOfAction = new TimeSpan(-1, 0, 0);
        }
    }
}

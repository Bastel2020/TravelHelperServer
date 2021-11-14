using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Database.Models
{
    public class TripAction
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public List<FileModel> Files { get; set; }
        public TimeSpan TimeOfAction { get; set; }
        [JsonIgnore]
        public TripDay Parent { get; set; }

        public TripAction(AddActionDTO data)
        {
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            TimeOfAction = data.TimeOfAction;
        }

        public TripAction() { }
    }
}

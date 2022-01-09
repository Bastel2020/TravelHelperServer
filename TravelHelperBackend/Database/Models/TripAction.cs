using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Database.Models
{
    public class TripAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public List<FileModel> Files { get; set; }
        public TimeSpan TimeOfAction { get; set; }
        public List<Poll> Polls { get; set; }
        [JsonIgnore]
        public TripDay Parent { get; set; }

        public TripAction(AddActionDTO data)
        {
            //Id = 99 + data.Name.Length;
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            TimeOfAction = TimeSpan.Parse(data.TimeOfAction);
        }
        public TripAction(AddActionDTO data, long id)
        {
            Id = id;
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            TimeOfAction = TimeSpan.Parse(data.TimeOfAction);
        }

        public TripAction(AddActionDTO data, TripDay parent)
        {
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            TimeOfAction = TimeSpan.Parse(data.TimeOfAction);
            Parent = parent;
        }

        public TripAction() { }
    }
}

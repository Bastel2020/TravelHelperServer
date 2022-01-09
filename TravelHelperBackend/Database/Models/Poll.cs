using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Database.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PollVariants> Variants { get; set; }
        [JsonIgnore]
        public TripAction Parent { get; set; }

        public Poll() { }

        public Poll(CreatePollDTO data)
        {
            Name = data.Name;
            Variants = data.PollVariants
                .Select(pv => new PollVariants() { Answer = pv, Votes = new List<User>() })
                .ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelHelperBackend.Database.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PollVariants> Variants { get; set; }
        [JsonIgnore]
        public Trip Parent { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelHelperBackend.Database.Models
{
    public class PollVariants
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public List<User> Votes { get; set; }
        [JsonIgnore]
        public Poll Parent { get; set; }
    }
}

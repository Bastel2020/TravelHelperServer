using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.Database.Models
{
    public class TripAction
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan TimeOfAction { get; set; }
        [field: NonSerialized]
        public TripDay Parent { get; set; }
    }
}

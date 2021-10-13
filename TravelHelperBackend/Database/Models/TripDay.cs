using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.Database.Models
{
    public class TripDay
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public List<TripAction> Actions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class VoteInPollDTO
    {
        [Required]
        public int PollId { get; set; }
        [Required]
        public int VariantIndex { get; set; }
    }
}

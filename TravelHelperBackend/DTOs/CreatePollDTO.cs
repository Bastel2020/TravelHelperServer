using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class CreatePollDTO
    {
        [Required]
        public long ActionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string[] PollVariants { get; set; }
    }
}

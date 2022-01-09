using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class ChangeRoleDTO
    {
        [Required]
        public int TripId { get; set; }
        [Required]
        public int UserToChangeId { get; set; }
        [Required]
        public int NewRoleId { get; set; }
    }
}

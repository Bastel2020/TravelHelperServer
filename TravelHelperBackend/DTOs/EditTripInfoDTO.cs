using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class EditTripInfoDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Destination { get; set; }

        public EditTripInfoDTO() { Destination = -1; } //Устанавливаем значение по умолчанию, чтобы при десериализации без этого поля город не менялся на город с Id 0.
    }
}

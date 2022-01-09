using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;

namespace TravelHelperBackend.DTOs
{
    public class ActionInfoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string TimeOfAction { get; set; }
        public string[] PathToFiles { get; set; }
        public object Poll { get; set; }

        public ActionInfoDTO(TripAction data)
        {
            Id = data.Id;
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            TimeOfAction = data.TimeOfAction.ToString(@"hh\:mm");

            if (data.Files != null)
                PathToFiles = data.Files
                    .Select(f => f.Path)
                    .ToArray();

            if (data.Polls != null && data.Polls.Count > 0)
            {
                var poll = data.Polls.First();
                Poll = new
                {
                    Id = poll.Id,
                    Name = poll.Name,                
                    Variants = poll.Variants.Select(v => new
                    {
                        Name = v.Answer,
                        VotersIds = v.Votes.Select(u => u.Id),
                        VotersUsernames = v.Votes.Select(u => u.Username)
                    })
                };
            }
        }
    }
}

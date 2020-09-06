using System;
using System.ComponentModel.DataAnnotations;

namespace BoardgameMeetup.Api.Models.Meetup
{
    public class CreateMeetupModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(2, int.MaxValue)]
        public int ParticipantCount { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public int Plz { get; set; }
    }
}

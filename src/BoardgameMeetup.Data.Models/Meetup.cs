using System;
using System.Collections.Generic;
using System.Text;

namespace BoardgameMeetup.Data.Models
{
    public class Meetup
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParticipantCount { get; set; }
        public string Place { get; set; }
        public int Plz { get; set; }
        public bool IsCancled { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}

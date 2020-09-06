using System;
using System.Collections.Generic;
using System.Text;

namespace BoardgameMeetup.Data.Models
{
    public class UserMeetup
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid MeetupId { get; set; }
        public virtual Meetup Meetup { get; set; }
    }
}

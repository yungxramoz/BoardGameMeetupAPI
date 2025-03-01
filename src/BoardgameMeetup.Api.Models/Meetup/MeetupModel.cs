﻿using System;

namespace BoardgameMeetup.Api.Models.Meetup
{
    public class MeetupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParticipantCount { get; set; }
        public string Place { get; set; }
        public int Plz { get; set; }
        
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}

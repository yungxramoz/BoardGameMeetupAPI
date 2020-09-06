using BoardgameMeetup.Data.Access.Maps.Common;
using BoardgameMeetup.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardgameMeetup.Data.Access.Maps.Main
{
    public class MeetupMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Meetup>()
                .ToTable("Meetups")
                .HasKey(x => x.Id);
        }
    }
}

using BoardgameMeetup.Data.Access.Maps.Common;
using BoardgameMeetup.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardgameMeetup.Data.Access.Maps.Main
{
    public class UserMeetupMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<UserMeetup>()
                .ToTable("UserMeetups")
                .HasKey(x => x.Id);
        }
    }
}

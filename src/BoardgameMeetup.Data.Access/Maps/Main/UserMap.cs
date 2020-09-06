using BoardgameMeetup.Data.Access.Maps.Common;
using Microsoft.EntityFrameworkCore;
using BoardgameMeetup.Data.Models;

namespace BoardgameMeetup.Data.Access.Maps.Main
{
    public class UserMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<User>()
                .ToTable("Users")
                .HasKey(x => x.Id);
        }
    }
}

using BoardgameMeetup.Data.Access.Maps.Common;
using BoardgameMeetup.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardgameMeetup.Data.Access.Maps.Main
{
    public class RoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .ToTable("Roles")
                .HasKey(x => x.Id);
        }
    }
}

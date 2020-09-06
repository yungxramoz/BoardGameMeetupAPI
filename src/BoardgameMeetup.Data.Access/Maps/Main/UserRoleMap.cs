using BoardgameMeetup.Data.Access.Maps.Common;
using BoardgameMeetup.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardgameMeetup.Data.Access.Maps.Main
{
    public class UserRoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                .ToTable("UserRoles")
                .HasKey(x => x.Id);
        }
    }
}

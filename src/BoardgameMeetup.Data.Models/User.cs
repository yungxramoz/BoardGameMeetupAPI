using System;
using System.Collections.Generic;
using System.Text;

namespace BoardgameMeetup.Data.Models
{
    public class User
    {
        public User()
        {
            Roles = new List<UserRole>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Plz { get; set; }
        public bool IsDeleted { get; set; }

        public virtual IList<UserRole> Roles { get; set; }
    }
}

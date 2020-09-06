﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BoardgameMeetup.Data.Models
{
    public class UserRole
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace BoardgameMeetup.Api.Models.User
{
    public class CreateUserModel
    {
        public CreateUserModel()
        {
            Roles = new string[0];
        }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }        
        [Required]
        public string LastName { get; set; }

        public string[] Roles { get; set; }
    }
}

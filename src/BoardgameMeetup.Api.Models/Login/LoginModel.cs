﻿using System.ComponentModel.DataAnnotations;

namespace BoardgameMeetup.Api.Models.Login
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

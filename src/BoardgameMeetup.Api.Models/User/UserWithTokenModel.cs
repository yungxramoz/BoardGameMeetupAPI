using System;

namespace BoardgameMeetup.Api.Models.User
{
    public class UserWithTokenModel
    {
        public string Token { get; set; }
        public UserModel User { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}

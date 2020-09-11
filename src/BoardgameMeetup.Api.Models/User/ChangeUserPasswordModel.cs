using System.ComponentModel.DataAnnotations;

namespace BoardgameMeetup.Api.Models.User
{
    class ChangeUserPasswordModel
    {
        [Required]
        public string Password { get; set; }
    }
}

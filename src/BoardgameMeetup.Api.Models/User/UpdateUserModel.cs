using System.ComponentModel.DataAnnotations;

namespace BoardgameMeetup.Api.Models.User
{
    public class UpdateUserModel
    {
        public UpdateUserModel()
        {   
            Roles = new string[0];
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string[] Roles { get; set; }
    }
}

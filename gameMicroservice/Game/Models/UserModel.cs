using System.ComponentModel.DataAnnotations;

namespace User.Models
{    /// Class contains informations about the game
    public class UserModel
    {
        /// ID primary key of the Game - unique player identifier
        [Key]
        public int Id { get; set; }
        /// Username of the user
        [Required]
        public string Username { get; set; }
        /// Contains hashed user password
        [Required]
        public string Password { get; set; }
        [Required]
        /// Contains user Role
        public string Role { get; set; }
        [Required]
        /// Boolean value that tells us if the user wants to receive email notifications.
        public bool Notifications { get; set; }
        [Required]
        //random string that is used to identify the user while providing the user with the option to enable or disable the notifications.
        public string NotificationToken { get; set; }
        /// Email address
        [Required]
        public string Email { get; set; }
    }
}
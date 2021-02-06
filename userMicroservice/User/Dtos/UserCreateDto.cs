namespace User.Dtos
{
    ///UserCreateDto class that represents the Data transfer object
    public class UserCreateDto
    {
        /// Represents the username.
        public string Username { get; set; }
        /// Represents the password.
        public string Password { get; set; }
        /// Represents the email.
        public string Email { get; set; }
        /// Represents the role.
        public string Role { get; set; }
    }
}
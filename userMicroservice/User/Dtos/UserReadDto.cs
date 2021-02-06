namespace User.Dtos
{
    ///UserCreateDto class that represents the Data transfer object
    public class UserReadDto
    {
        /// Represents the id.
        public int Id { get; set; }
        /// Represents the username.
        public string Username {get; set;}
        /// Represents the email.
        public string Email { get; set; }
        /// Represents the role.
        public string Role { get; set; }
       
    }
}
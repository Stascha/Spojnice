namespace User.Dtos
{
    ///UserCreateDto klasa predstavlja objekat za transfer podataka
    public class UserReadDto
    {
        /// Predstavlja id.
        public int Id { get; set; }
        /// Predstavlja username.
        public string Username {get; set;}
        /// Predstavlja email.
        public string Email { get; set; }
        /// Predstavlja role.
        public string Role { get; set; }
        // Predstavlja notification token
        public string NotificationToken { get; set; }
        /// Boolean vrednost koja predstavlja da li igrac zeli da dobija email notificatikacija.
        public bool Notifications { get; set; }
    }
}
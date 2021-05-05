namespace User.Dtos
{
    /// UserCreateDto klasa predstavlja objekat za transfer podataka
    public class UserCreateDto
    {
        /// Predstavlja korisnicko ime.
        public string Username { get; set; }
        /// Predstavlja lozinku.
        public string Password { get; set; }
        /// Predstavlja email.
        public string Email { get; set; }
        /// Predstavlja informaciju da li je igrac admin ili ne.
        public string Role { get; set; }

    }
}
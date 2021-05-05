namespace Game.Dtos
{
    /// UserCreateDto klasa predstavlja objekat za transfer podataka
    public class GameCreateDto
    {
        /// Predestavlja naziv igre.
        public string Name { get; set; }
        /// Predstavlja podatke igre, Json kao string.
        public string Data { get; set; }
    }
}
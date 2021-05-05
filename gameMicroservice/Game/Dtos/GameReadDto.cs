namespace Game.Dtos
{
    ///ScoreReadDto klasa predstavlja objekat za transfer podataka
    public class GameReadDto
    {
        /// Predstavlja id.
        public int Id { get; set; }
        /// Predstavlja naziv igre.
        public string Name { get; set; }
        /// Predstavlja podatke igre, Json kao string.
        public string Data { get; set; }
    }
}
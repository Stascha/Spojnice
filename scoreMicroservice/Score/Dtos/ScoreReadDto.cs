using Score.Models;
namespace Score.Dtos
{
    ///  ScoreReadDto klasa predstavlja objekat za transfer podataka
    public class ScoreReadDto
    {
        /// Predstavlja id.
        public int Id { get; set; }
        /// Predstavlja Score.
        public int Score { get; set; }
        /// Predstavlja korisnicko ime.
        public string Username { get; set; }
       
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace Score.Dtos
{
    /// UserCreateDto klasa predstavlja objekat za transfer podataka
    public class ScoreCreateDto
    {
        /// Predstavlja skor.
        public int Score { get; set; }

        /// Predstavlja korisnicko ime.
        public string Username { get; set; }
    }
}
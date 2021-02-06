using System.ComponentModel.DataAnnotations.Schema;

namespace Score.Dtos
{
    ///ScoreCreateDto class that represents the Data transfer object
    public class ScoreCreateDto
    {
        /// Represents the Score.
        public int Score { get; set; }

        /// Represents the Username.
        public string Username { get; set; }
    }
}
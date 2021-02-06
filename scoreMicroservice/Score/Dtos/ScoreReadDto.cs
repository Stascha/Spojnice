using Score.Models;
namespace Score.Dtos
{
    ///ScoreReadDto class that represents the Data transfer object
    public class ScoreReadDto
    {
        /// Represents the id.
        public int Id { get; set; }
        /// Represents the Score.
        public int Score { get; set; }
        /// Represents the Username.
        public string Username { get; set; }
       
    }
}
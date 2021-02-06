using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Score.Models
{
    /// Class contains informations about the score
    public class ScoreModel
    {
        /// ID primary key of the Score - unique player identifier
        [Key]
        public int Id { get; set; }

        /// score - number of scored points
        [Required]
        public int Score { get; set; }

        /// Contains username
        [Required]
        public string Username { get; set; }
    }
}
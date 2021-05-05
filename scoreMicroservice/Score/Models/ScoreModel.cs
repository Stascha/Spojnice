using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Score.Models
{
    /// Klasa sadrzi informacije o skoru   
    public class ScoreModel
    {
        /// ID primary key igraca - jedinstveni identifikator igraca
        [Key]
        public int Id { get; set; }

        /// skor - broj poena
        [Required]
        public int Score { get; set; }

        /// Korisnicko ime igraca
        [Required]
        public string Username { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Game.Models
{
    /// Klasa sadrzi informacije o igri
    public class GameModel
    {
        /// ID primary key igre - jedinstveni identifikator igre
        [Key]
        public int Id { get; set; }
        // Naziv igre
        [Required]
        public string Name { get; set; }
        // Podaci za igru su JSON kao string. Podaci su odgovarajuci parovi.
        [Required]
        public string Data { get; set; }
    }
}
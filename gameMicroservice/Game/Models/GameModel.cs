using System.ComponentModel.DataAnnotations;

namespace Game.Models
{
    /// Class contains informations about the game
    public class GameModel
    {
        /// ID primary key of the Game - unique game identifier
        [Key]
        public int Id { get; set; }
        //Name of the game
        [Required]
        public string Name { get; set; }
        // Game Data contains JSON like structure as string. That has the data about matching pairs.
        [Required]
        public string Data { get; set; }
    }
}
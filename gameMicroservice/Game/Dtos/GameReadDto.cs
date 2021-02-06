namespace Game.Dtos
{
    ///GameReadDto class that represents the Data transfer object
    public class GameReadDto
    {
        /// Represents the id.
        public int Id { get; set; }
        /// Represents the game name.
        public string Name { get; set; }
        /// Represents the game data, Json in a string.
        public string Data { get; set; }
    }
}
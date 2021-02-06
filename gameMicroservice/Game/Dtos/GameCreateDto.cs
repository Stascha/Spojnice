namespace Game.Dtos
{
    ///GameCreateDto class that represents the Data transfer object
    public class GameCreateDto
    {
        /// Represents the game name.
        public string Name { get; set; }
        /// Represents the game data, Json in a string.
        public string Data { get; set; }
    }
}
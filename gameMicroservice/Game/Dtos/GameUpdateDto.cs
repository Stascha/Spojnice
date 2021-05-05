namespace Game.Dtos
{
     ///GameReadDto class that represents the Data transfer object
        public class GameUpdateDto
    {
        /// Predstavlja id.
        public int Id { get; set; }
        /// Predstavlja naziv igre.
        public string Name { get; set; }
        /// Predstavlja podatke igre, Json kao string.
        public string Data { get; set; }
    }
}
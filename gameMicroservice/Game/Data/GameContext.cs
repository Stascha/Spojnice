using Game.Models;
using Microsoft.EntityFrameworkCore;
using User.Models;

namespace Game.Data
{
    /// DataBase Context klasa : SlagalicaDB
    public class GameContext : DbContext
    {
        /// Konstruktor GameContext klase
        public GameContext(DbContextOptions<GameContext> opt) : base(opt)
        {
            
        }
        /// DbSet igara
        public DbSet<GameModel> Games { get; set; }
        /// DbSet igraca
        public DbSet<UserModel> Users { get; set; }
    }
}
using Game.Models;
using Microsoft.EntityFrameworkCore;
using User.Models;

namespace Game.Data
{
    /// DataBase Context Class : SlagalicaDB
    public class GameContext : DbContext
    {
        /// Constructor of DataBase Context Class
        public GameContext(DbContextOptions<GameContext> opt) : base(opt)
        {
            
        }
        /// DbSet of Games
        public DbSet<GameModel> Games { get; set; }
        /// DbSet of Users
        public DbSet<UserModel> Users { get; set; }
    }
}
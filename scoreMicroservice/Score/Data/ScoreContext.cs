using Score.Models;
using Microsoft.EntityFrameworkCore;
using User.Models;

namespace Score.Data
{
    /// DataBase Context Klasa : SlagalicaDB
    public class ScoreContext : DbContext
    {
        /// Konstruktor ScoreContext Klase
        public ScoreContext(DbContextOptions<ScoreContext> opt) : base(opt)
        {
            
        }
        /// DbSet Skoreva
        public DbSet<ScoreModel> Score { get; set; }

        /// DbSet Igraca
        public DbSet<UserModel> Users { get; set; }
    }
}
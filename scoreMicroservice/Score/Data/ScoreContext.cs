using Score.Models;
using Microsoft.EntityFrameworkCore;
using User.Models;

namespace Score.Data
{
    /// DataBase Context Class : SlagalicaDB
    public class ScoreContext : DbContext
    {
        /// Constructor of DataBase Context Class
        public ScoreContext(DbContextOptions<ScoreContext> opt) : base(opt)
        {
            
        }
        /// DbSet of Score
        public DbSet<ScoreModel> Score { get; set; }

        /// DbSet of Users
        public DbSet<UserModel> Users { get; set; }
    }
}
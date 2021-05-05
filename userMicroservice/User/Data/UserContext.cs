using Microsoft.EntityFrameworkCore;
using User.Models;

namespace User.Data
{
    /// DataBase Context klasa : SlagalicaDB
    public class UserContext : DbContext
    {
        /// Konstruktor za UserContext klasu
        public UserContext(DbContextOptions<UserContext> opt) : base(opt)
        {
            
        }

        /// DbSet korisnika
        public DbSet<UserModel> Users { get; set; }
        
    }
}
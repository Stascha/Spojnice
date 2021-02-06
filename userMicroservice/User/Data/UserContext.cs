using Microsoft.EntityFrameworkCore;
using User.Models;

namespace User.Data
{
    /// DataBase Context Class : SlagalicaDB
    public class UserContext : DbContext
    {
        /// Constructor of DataBase Context Class
        public UserContext(DbContextOptions<UserContext> opt) : base(opt)
        {
            
        }

        /// DbSet of Users
        public DbSet<UserModel> Users { get; set; }
        
    }
}
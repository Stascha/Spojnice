using User.Models;
namespace User.Data
{
    /// Interface that SqlUserRepo is going to implmenet and thus will need to have all the methods described.
    public interface IUserRepo
    {
        bool SaveChanges();
        UserModel GetUserByUsernameAndPassword(string username, string pwd);
        void CreateUser(UserModel usr);
        UserModel GetUserByUsername(string usr);
        UserModel GetUserByEmail(string email);
        UserModel GetUserByNotificationToken(string token);
    }
}
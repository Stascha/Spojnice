using User.Models;
namespace User.Data
{
    /// Interfejs koji SqlUserRepo implmenetira i definise sve metode.
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
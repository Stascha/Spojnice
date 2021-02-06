using System;
using System.Linq;
using User.Models;

namespace User.Data
{
    /// SQL Game Repository class
    public class SqlUserRepo : IUserRepo
    {
        /// _context contains our database context.
        private readonly UserContext _context;

        /// Constructor of SqlGameRepo Class
        public SqlUserRepo(UserContext context)
        {
            _context = context;
        }

        /** ### Description
        * CreateUser method - creates new user from the UserModel object received. Throws error if UserModel object is not provided.
        * ### Arguments
        * UserModel usr - user to add <br>
        * ### Return value
        * None.*/
        public void CreateUser(UserModel usr)
        {
            if (usr==null){
                throw new ArgumentNullException(nameof(usr));
            }
            _context.Users.Add(usr);
        }

        /** ### Description
        * GetUserByUsernameAndPassword method - returns the user if password hash matches the username in the database
        * ### Arguments
        * string usr - represents the username </br>
        * string pwd - represents the password in plain text</br>
        * ### Return value
        * UserModel - returns user object if password hash in the database matches the plain
        * password sent as input as well as if it matches the username. If username and password
        * don't match or if username does not exist null will be returned.*/
        public UserModel GetUserByUsernameAndPassword(string usr, string pwd)
        {
            var account = _context.Users.SingleOrDefault(p => p.Username == usr);

            if (account == null || !BCrypt.Net.BCrypt.Verify(pwd, account.Password))
            {
                return null;
            }
            else
            {
                return account;
            }
        }

        /** ### Description
        * GetUserByUsername method - returns object if provided username exists in the database.
        * ### Arguments
        * string usr - username as input </br>
        * ### Return value
        * UserModel - if user exists in the database user object is returned, otherwise null is returned.*/
        public UserModel GetUserByUsername(string usr)
        {
            return _context.Users.FirstOrDefault(p => p.Username == usr );
        }

        /** ### Description
        * GetUserByEmail method - returns object if provided email exists in the database.
        * ### Arguments
        * string email - email as input </br>
        * ### Return value
        * UserModel - if user exists in the database user object is returned, otherwise null is returned.*/

        public UserModel GetUserByEmail(string email){
            return _context.Users.FirstOrDefault(p => p.Email == email );
        }

        /** ### Description
        * GetUserByNotificationToken method - returns object if notification token exists.
        * ### Arguments
        * string token - token string as input </br>
        * ### Return value
        * UserModel - if notification token exists in the database user object is returned, otherwise null is returned.*/
        public UserModel GetUserByNotificationToken(string token){
            return _context.Users.SingleOrDefault(p => p.NotificationToken == token);
        }

        /** ### Description
        * SaveChanges method - saves changes that are made to our _context.
        * ### Arguments
        * None.
        * ### Return
        * (_context.SaveChanges() >= 0);*/
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
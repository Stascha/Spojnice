using System;
using System.Linq;
using User.Models;

namespace User.Data
{
    /// SQL Repository klasa
    public class SqlUserRepo : IUserRepo
    {
        /// _context sadrzi database context.
        private readonly UserContext _context;

        /// Konstruktor SqlUserRepo Klase
        public SqlUserRepo(UserContext context)
        {
            _context = context;
        }

        /** ### Description
        * CreateUser method - Kreira novog igraca iz dobijenog  UserModel objekta. \n 
        * Izbacuje error ako nema UserModel objekta.
        * ### Arguments
        * UserModel usr - Igrac koji se dodaje <br>
        * ### Return value
        * Nema.*/
        public void CreateUser(UserModel usr)
        {
            if (usr==null){
                throw new ArgumentNullException(nameof(usr));
            }
            _context.Users.Add(usr);
        }

        /** ### Description
        * GetUserByUsernameAndPassword metod - Prikazuje igraca sa tim korisnickim imenom i 
        * lozinkom ako lozika odgovara hesiranoj lozinki iz baze podataka
        * ### Arguments
        * string usr - Korisnicko ime \n 
        * string pwd - Lozinka koja nije hesirana<br>
        * ### Return value
        * UserModel - Vraca objekat igrac ako ne hesirana lozinka odgovara hesiranoj lozinki iz baze podataka
        * Ako korisnicko ime i lozinka ne odgovaraju ili korisnicko ime ne postoji vraca null */
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
        * GetUserByUsername method - Vraca objekat igraca ako prosledjeno korisnicko ime postoji u bazi podataka.
        * ### Arguments
        * string usr - Korisnicko ime </br>
        * ### Return value
        * UserModel - Vraca objekat igraca ako prosledjeno korisnicko ime postoji u bazi podataka, inace vraca null.*/
        public UserModel GetUserByUsername(string usr)
        {
            return _context.Users.FirstOrDefault( p => p.Username == usr );
        }

        /** ### Description
        * GetUserByEmail method -Vraca objekat igraca ako prosledjena email adresa postoji u bazi podataka, inace vraca null. 
        * ### Arguments
        * string email - email adresa </br>
        * ### Return value
        * UserModel - Vraca objekat igraca ako prosledjena email adresa postoji u bazi podataka, inace vraca null.*/

        public UserModel GetUserByEmail(string email){
            return _context.Users.FirstOrDefault(p => p.Email == email );
        }

        /** ### Description
        * GetUserByNotificationToken method - Vraca objekat igraca ako prosledjeni notification token postoji u bazi podataka.
        * ### Arguments
        * string token - notification token </br>
        * ### Return value
        * UserModel - Vraca objekat igraca ako prosledjeni notification token postoji u bazi podataka, inace vraca null.*/
        public UserModel GetUserByNotificationToken(string token){
            return _context.Users.SingleOrDefault(p => p.NotificationToken == token);
        }

        /** ### Description
        * SaveChanges method - Cuva promene koje su napravljene u _context.
        * ### Arguments
        * Nema.
        * ### Return
        * (_context.SaveChanges() >= 0);*/
        public bool SaveChanges()
        {
            return ( _context.SaveChanges() >= 0 );
        }
    }
}

using User.Models;
using System.Net.Mail;

namespace User.Data
{
    /// Klasa koja sadrzi metod za slanje email poruka preko SMTP protokola
    public class UserEmailSender
    {
       
        /// Email adresa sa koje aplikacija slje meilove
        private string myEmail;
        /// Lozinka za email adresu sa koje aplikacija salje meilove
        private string myEmailPassword;
        /// URL adresa do smtp servera
        private string smtpURL;
        /// Smtp port za smtp server koji aplikacija koristi za slanje emailova.
        private int smtpPort;
        /// Koriscenje secure konekcije ili ne.
        private bool smtpEnableSSL;


        /// Konstruktor UserEmailSender klase
        public UserEmailSender()
      {
            this.myEmail = "slagalica@airmail.cc";
            this.myEmailPassword = "richgrain16lushkite94";
            this.smtpURL = "mail.cock.li";
            this.smtpPort = 587;
            this.smtpEnableSSL = true;   
        }

        /** ### Description
       * SendNoLongerFirstMail - Poslace e-mail Igracu koji ce sadrzati poruku 
       * da on sada nije vise prvi na skor tabeli. \n 
       * E-mail ce takodje sadrzati link za odjavljivanje od dobijanja notifikacija od aplikacije.
       * ### Arguments
       * UserModel account- korisnik kome se salje email \n 
       * string currentFirst - sadrzi korisnicko ime igraca koji je sada na prvom mestu.
       * ### Return value
       * Nema. */
        public void SendNoLongerFirstMail(UserModel account, string currentFirst){
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(this.smtpURL);

            mail.From = new MailAddress( this.myEmail);
            mail.To.Add(account.Email);
            mail.Subject = "Spojnice - Obavestenje! Niste Vise Prvi Na Score Tabeli Igre Spojnice !";
            mail.Body = "Zdravo,"
                        + "\n\nNiste Vise Prvi Na Score Tabeli Igre Spojnice !"
                        + "\nIgrac sa korisnickim imenom " + currentFirst + " je sada prvi na tabeli!!!"
                        + "\nAko ne zelite više da dobijate obaveštenja u vezi igre spojnice kliknite na link https://localhost:5101/api/users/notification/change/" + account.NotificationToken + "/false"
                        + "\n\n\nBest Regards,"
                        + "\nIgra Spojnice";




            SmtpServer.Port = this.smtpPort;
            SmtpServer.Credentials = new System.Net.NetworkCredential( this.myEmail, this.myEmailPassword);
            SmtpServer.EnableSsl = this.smtpEnableSSL;

            SmtpServer.Send(mail);

        }
        
    }
}
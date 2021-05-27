
using User.Models;
using System.Net.Mail;
using System;

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

        /// Konstruktor za UserEmailSender klasu
        
        public UserEmailSender()
        {
            this.myEmail = "berlin.alexanderplatz@outlook.com";
            this.myEmailPassword = "Ganze_Nacht _Feiern_77!!";
            this.smtpURL = "smtp.live.com";
            this.smtpPort = 25;
            this.smtpEnableSSL = true;   
        }

        /*
       {
           this.myEmail = "couplings.newyork@gmail.com";
           this.myEmailPassword = "NewYork40$$";
           this.smtpURL = "smtp.gmail.com";
           this.smtpPort = 465;
           this.smtpEnableSSL = true;   
        } 
        */

       /** ### Description
       * SendWelcomeMail - Poslace email poruku igracu koji se upravo registrovao. \n  
       * Email poruka ce sadrzati link koji ce igracu omoguciti da dobija notifikacije od aplikacije.
       * ### Arguments
       * UserModel account - Igrac objekat kome ce poslati email</br>
       * ### Return value
       * Nista. */
        public void SendWelcomeMail(UserModel account){
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(this.smtpURL);

            mail.From = new MailAddress( this.myEmail);
            mail.To.Add(account.Email);
            mail.Subject = "Spojnice - Dobrodosli!";
            mail.Body = "Uspesno Vam je napravnjen nalog!\nUsername: " + account.Username 
                        + "\nObavestenja: neaktivna"
                        + "\nUkoliko su obavestenja aktivna, dobicete mail:"
                        + "\n   - Svaki put kada se kreira nova igra"
                        + "\n   - Svaki put kada neko zauzme prvo mesto, ukoliko ste Vi bili na prvom mestu"
                        + "\nAko zelite da dobijate obaveštenja u vezi igre spojnice kliknite na link https://localhost:5101/api/users/notification/change/" + account.NotificationToken + "/true"
                        + "\n\n\nBest Regards,"
                        + "\nIgra Spojnice";

            SmtpServer.Port = this.smtpPort;
            SmtpServer.Credentials = new System.Net.NetworkCredential( this.myEmail, this.myEmailPassword);
            SmtpServer.EnableSsl = this.smtpEnableSSL;

            SmtpServer.Send(mail);
           
        }
        
    }
}

using User.Models;
using System.Net.Mail;
using System.Collections.Generic;

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
       * SendNotificationNewGameCreatedMail ce poslati email poruke prosledjenoj listi igraca.  \n
       * Email poruke ce sadrzati informaciju da je kreirana nova igra i ime nove igre. \n 
       * E-mail poruke ce takodje sadrzati link za odjavljivanje od dobijanja notifikacija od aplikacije.
       * ### Arguments
       * IEnumerable<UserModel> accounts - lista UserModels, lista igraca kojima ce biti poslati meilovi. \n 
       * string gameName - Ime nove igre koja je kreirana. \n 
       * ### Return value
       * Nema. */
        public void SendNotificationNewGameCreatedMail(IEnumerable<UserModel> accounts, string gameName){
            SmtpClient SmtpServer = new SmtpClient(this.smtpURL);
            SmtpServer.Port = this.smtpPort;
            SmtpServer.Credentials = new System.Net.NetworkCredential( this.myEmail, this.myEmailPassword);
            SmtpServer.EnableSsl = this.smtpEnableSSL;
            foreach (UserModel account in accounts)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress( this.myEmail);
                mail.To.Add(account.Email.Replace("\r\n", System.String.Empty));
           
                mail.Subject = "Spojnice - Obavestenje! Nova Igra je dodata!";
                mail.Body = "Sada je dodata nova igra pod nazivom: " + gameName
                            + "\nPuno srece Vam zelimo."
                            + "\nAko ne zelite više da dobijate obaveštenja u vezi igre spojnice kliknite na link https://localhost:5101/api/users/notification/change/" + account.NotificationToken + "/false"
                            + "\n\n\nBest Regards,"
                            + "\nIgra Spojnice";

                SmtpServer.Send(mail);
             }

        }
        
    }
}

using User.Models;
using System.Net.Mail;
using System.Collections.Generic;

namespace User.Data
{
    /// Class that contains methods to send an email via SMTP protocol
    public class UserEmailSender
    {
        /// Our email from which we want to send emails.
        private string myEmail;
        /// Our email password.
        private string myEmailPassword;
        /// Url address to the smtp server that the set email is using.
        private string smtpURL;
        /// Smtp port for the smtp server that we use to send out emails.
        private int smtpPort;
        /// Use secure connection or not.
        private bool smtpEnableSSL;
        /// Constructor for the UserEmailSender class
        public UserEmailSender()
        {
            this.myEmail = "slagalica@airmail.cc";
            this.myEmailPassword = "richgrain16lushkite94";
            this.smtpURL = "mail.cock.li";
            this.smtpPort = 587;
            this.smtpEnableSSL = true; 
        } 
     /* {
            this.myEmail = "couplings.newyork@gmail.com";
            this.myEmailPassword = "NewYork40$$";
            this.smtpURL = "smtp.gmail.com";
            this.smtpPort = 465;
            this.smtpEnableSSL = true;   
        } */

    /** ### Description
    * SendNotificationNewGameCreatedMail is going to send emails to the provided list of users. Email will contain the new created game name as well as additional options to disable or enable future notifications.
    * ### Arguments
    * IEnumerable<UserModel> accounts - list of UserModels, list of users to who to send emails to. </br>
    * string gameName - game name that represents the name of the game that is created. </br>
    * ### Return value
    * None. */
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
                            + "\nPuno srece Vam zelimo da ostvarite sto bolji rezultat."
                            + "\nAko ne zelite više da dobijate obaveštenja u vezi igre spojnice kliknite na link https://localhost:5101/api/users/notification/change/" + account.NotificationToken + "/false"
                            + "\n\n\nBest Regards,"
                            + "\nIgra Spojnice";

                SmtpServer.Send(mail);
             }

        }
        
    }
}
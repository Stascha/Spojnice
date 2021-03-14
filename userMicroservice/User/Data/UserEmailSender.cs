
using User.Models;
using System.Net.Mail;
using System;

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
    * SendWelcomeMail - is going to send an welcome email to the email extracted from user object. Email will contain links to enable and disable notifications in future.
    * ### Arguments
    * UserModel account - usr object to send an email to </br>
    * ### Return value
    * None. */
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
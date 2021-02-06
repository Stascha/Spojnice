
using User.Models;
using System.Net.Mail;

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
         /** ### Description
        * SendNoLongerFirstMail - Is going to send an email to an object user provided.
        * Email will contain notificationn message that some other user has overtaken the lead in the Score table.
        * Within the email options will be provided to enable/disable future subscription.
        * ### Arguments
        * UserModel account- user object model to who to send an email</br>
        * string currentFirst - contains the username of the user who is at the first position now.
        * ### Return value
        * None. */
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
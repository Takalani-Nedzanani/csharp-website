using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace firebase1.Repository.Utilities
{
    static public class Mails
    {
        public static Task SendMail(MailMessage mailMessage) 
        {
            var credentialUserName = mailMessage.To;
            var sendFrom = mailMessage.From;
            var pwd = "EduConnectAdmin";


            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            NetworkCredential credential = new NetworkCredential(credentialUserName.ToString(), pwd);
            client.EnableSsl = true;
            client.Credentials = credential;
            
            var mail = new MailMessage(mailMessage.From.ToString(), mailMessage.To.ToString());
            mail.From = new MailAddress(mailMessage.From.ToString(),"EduConnect");
            mail.Subject = mailMessage.Subject;
            mail.Body = mailMessage.Body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mail.ReplyToList.Add("noreply_Educonnect");


            return client.SendMailAsync(mail);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LearningManagementSystem.MVC.Notification
{
    public class Mail
    {
        public static bool SendMailToUser(string ToAddress, string UserPassword)
        {
            try
            {
                string MessageBody = "Thank You For Registering\n\nYou Can Login By Using Following Credentials\n" +
                    "UserId : " + ToAddress + "\nPassword : " + UserPassword;
                string Password = "snhidnogvbmytasm";
                string FromAddress = "manger.lms@gmail.com";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(FromAddress);
                message.To.Add(new MailAddress(ToAddress));
                message.Subject = "User Credentials";
                message.Body = MessageBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(FromAddress, Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

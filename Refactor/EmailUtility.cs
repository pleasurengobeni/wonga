using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Refactor
{
    public class EmailUtility
    {
        public static bool SendEmail(string mailto, string subject, string Message)
        {
            bool EmailSent = false;
            try
            {
                System.Net.NetworkCredential NetworkCredential = new System.Net.NetworkCredential();
                NetworkCredential.Password = ConfigurationManager.AppSettings["SMTPpassword"];
                NetworkCredential.UserName = ConfigurationManager.AppSettings["SMTPuser"];
                System.Net.Mail.MailMessage mailmsg = new System.Net.Mail.MailMessage();

                mailmsg.Body = Message;
                mailmsg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["SMTPFrom"]);
                mailmsg.To.Add(mailto);
                System.Net.Mail.SmtpClient s = new System.Net.Mail.SmtpClient();
                s.UseDefaultCredentials = false;
                s.Credentials = NetworkCredential;
                s.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                s.EnableSsl = false;
                s.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPserverport"]);
                s.Host = ConfigurationManager.AppSettings["SMTPserver"];
                
                s.Send(mailmsg);
                mailmsg = null;
            }
            catch (Exception ex)
            {
                EmailSent = false;
                Console.WriteLine(ex.ToString());
            }
            return EmailSent;
        }
    }
}

using MMCApp.Infrastructure.ApplicationServices.Contracts;
using System;
using System.Net.Mail;

namespace MMCApp.Infrastructure.ApplicationServices
{
    public class EMailService : IEMail
    {
        public void SendEmail(string msg, string sender, string EMailCc, string subject, string[] AttachDoc)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["FromAddress"]);
                sender = sender.Substring(1, sender.Length - 2);
                string[] emailto = sender.Split(',');
                foreach (var email in emailto)
                {
                    mail.To.Add(email.ToString().Substring(1, email.ToString().Length - 2));
                }
                if (EMailCc.Length > 2)
                {
                    EMailCc = EMailCc.Substring(1, EMailCc.Length - 2);
                    string[] emailcc = EMailCc.Split(',');
                    foreach (var emailc in emailcc)
                    {
                        mail.CC.Add(emailc.ToString().Substring(1, emailc.ToString().Length - 2));
                    }
                }

                mail.Subject = subject;
                mail.Body = msg;
                mail.IsBodyHtml = true;
                foreach (var docmt in AttachDoc)
                {
                    mail.Attachments.Add(new Attachment(docmt.ToString()));
                }
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"];
                smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["MailPort"]);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["MailUserName"], System.Configuration.ConfigurationSettings.AppSettings["MailPassword"]);
                smtp.Send(mail);
                mail.Dispose();
            }
        }
    }
}

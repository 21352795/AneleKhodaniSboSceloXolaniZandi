using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Template.BusinessLogic.Implementation
{
    public class EmailBusiness
    {
        public MailAddress to { get; set; }
        public MailAddress from { get; set; }
        public string sub { get; set; }
        public string body { get; set; }

        public string Notification()
        {
            SmtpClient client = new SmtpClient();
            string feedback = "";
            var m = new MailMessage()
            {
                Subject = sub,
                Body=body, 
                IsBodyHtml = true
            };

            m.From = new MailAddress("21004591@dut4life.ac.za", "");
            m.To.Add(to);
            SmtpClient smtp = new SmtpClient()
            {
                Host = "pod51014.outlook.com",
                Port = 587,
                Credentials = new NetworkCredential("21004591@dut4life.ac.za", "Dut911224"),
                EnableSsl = true
            };

            try
            {
                smtp.Send(m);
                feedback = "Email sent T" + to;
            }
            catch (Exception ex)
            {
                feedback = "Message not sent" + ex.Message;
            }
            return feedback;
        }

        public string NotificationWithAttachment()
        {
            SmtpClient client = new SmtpClient();
            string feedback = "";
            var m = new MailMessage()
            {
                Subject = sub,
                Body = body,
                IsBodyHtml = true
            };

            m.From = new MailAddress("21004591@dut4life.ac.za", "");
            m.To.Add(to);
            Attachment attachment;
            attachment = new Attachment(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Policy Document1.pdf"));
            m.Attachments.Add(attachment);
            SmtpClient smtp = new SmtpClient()
            {
                Host = "pod51014.outlook.com",
                Port = 587,
                Credentials = new NetworkCredential("21004591@dut4life.ac.za", "Dut911224"),
                EnableSsl = true
            };

            try
            {
                smtp.Send(m);
                feedback = "Email sent T" + to;
            }
            catch (Exception ex)
            {
                feedback = "Message not sent" + ex.Message;
            }
            return feedback;
        }

        public bool SendEmailForgot(string email, string callbackUrl)
        {
            bool isvalid = false;
            SmtpClient client = new SmtpClient();
            try
            {
                var boddy = new StringBuilder();

                boddy.Append("Hi! " + email + "<br/>");
                boddy.Append("Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                boddy.Append("<br/><br/> Regards<br/><b>Mpiti Funeral Undertakers - Security Team</b>");
                var m = new MailMessage()
                {
                    Subject = "Reset Password - Mpiti Funeral Undertakers",
                    Body = boddy.ToString(),
                    IsBodyHtml = true
                };

                m.From = new MailAddress("21004591@dut4life.ac.za", "");
                m.To.Add(email);
                SmtpClient smtp = new SmtpClient()
                {
                    Host = "pod51014.outlook.com",
                    Port = 587,
                    Credentials = new NetworkCredential("21004591@dut4life.ac.za", "Dut911224"),
                    EnableSsl = true
                };
                smtp.Send(m);
                isvalid = true;
                return isvalid;
            }
            catch
            {
                return isvalid;
            }
        }

    }
}
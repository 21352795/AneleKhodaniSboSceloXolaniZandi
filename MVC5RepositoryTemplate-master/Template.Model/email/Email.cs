using System.Text;
using Template.Data;

//using System.Web.Helpers;

namespace Template.Model.email
{
    public class Email
    {
        public  void IsValid(string to, string firstname)
        {
           
            StringBuilder body = new StringBuilder();
            ClientApplication cp = new ClientApplication();
           
            body.Append("Hi!" + firstname + "<br/>");
            string ToFor = to;
            string subjectFor = "Confirmation for Registration";
            string bodyFor = "Thank you for registering ";

            //WebMail.SmtpServer = "pod51014.outlook.com";
            //WebMail.SmtpPort = 587;
            //WebMail.UserName = "21004591@dut4life.ac.za";
            //WebMail.Password = "Dut911224";
            //WebMail.From = "21004591@dut4life.ac.za";
            //WebMail.EnableSsl = true;

            //WebMail.Send(to: ToFor, subject: subjectFor, body: bodyFor);
        }
    }
}
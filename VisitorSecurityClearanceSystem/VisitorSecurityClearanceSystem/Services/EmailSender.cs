using System.Net.Mail;
using VisitorSecurityClearanceSystem.Common;
using SendGrid;
using SendGrid.Helpers.Mail;

using System.Net.Mail;
namespace VisitorSecurityClearanceSystem.Services
{
    public class EmailSender
    {
        public async Task SendEmail(string subject, string toEmail, string userName, string message)
        {
            var apiKey = Credentials.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tidkeshubham10@gmail.com", "VisitorSecurityClearanceSystem");
            /*var subject = "Sending with send grid in fun";*/
            var to = new EmailAddress(toEmail, userName);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}

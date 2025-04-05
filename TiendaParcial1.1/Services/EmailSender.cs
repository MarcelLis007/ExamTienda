using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace TiendaParcial1._1.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
          
            Console.WriteLine($"Sending email to {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {htmlMessage}");

            return Task.CompletedTask; 
        }
    }
}
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace TiendaParcial1._1.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Aquí puedes implementar el envío real de correos electrónicos.
            // Por ahora, simplemente mostraremos los detalles en la consola.
            Console.WriteLine($"Sending email to {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {htmlMessage}");

            return Task.CompletedTask; // Simulamos que el correo fue enviado
        }
    }
}
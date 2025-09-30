using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Vetsys.API.Modules.Notifications.Contracts;  

namespace Vetsys.API.Modules.Notifications.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Vetsys", "")); // Cambia por tu remitente
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false); // Cambia host y puerto
            await client.AuthenticateAsync("taskplannerapplication@gmail.com", "umog cbzy wwlt atda"); // Cambia usuario y contraseña
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

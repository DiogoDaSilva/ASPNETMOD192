using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace ASPNETMOD192.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Credentials = new NetworkCredential("aspnetMOD192DiogoSilva@gmail.com", "eqnkrhcsuuirpusj"),
                Port = 587,
                EnableSsl = true,
            };


            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress("aspnetMOD192DiogoSilva@gmail.com", "Seguro Saúde Municipal"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };


            mailMessage.To.Add(email);

            mailMessage.Bcc.Add("aspnetMOD192DiogoSilva@gmail.com");

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Task.CompletedTask;
        }
    }
}

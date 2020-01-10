using System.Threading.Tasks;

namespace Forum.Services
{
    using Forum.Entities;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;
    using System;
    using System.Net;
    using System.Net.Mail;

    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostingEnvironment _env;
        public EmailSender(
        IOptions<EmailSettings> emailSettings,
        IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        public EmailSender(string host, int port, bool enableSSL, string userName, string password) {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            {
                var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = enableSSL
                };
                try
                {
                    MailMessage telegram = new MailMessage(userName, email, subject, message);
                    client.Send(telegram);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Email can not be sended. Please try again later.",ex);
                }
                return Task.CompletedTask;
            }
            
        }
    }
}

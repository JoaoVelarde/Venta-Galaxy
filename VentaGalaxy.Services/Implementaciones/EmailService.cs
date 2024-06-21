
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Shared.Configuracion;

namespace VentaGalaxy.Services.Implementaciones;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly SmtpConfiguration _smtpConfiguration;

    public EmailService(ILogger<EmailService> logger, IOptions<AppSettings> options)
    {
        _logger = logger;
        _smtpConfiguration = options.Value.SmtpConfiguration;
    }
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            var smtpClient = new SmtpClient(_smtpConfiguration.Servidor)
            {
                Port = _smtpConfiguration.Puerto,
                Credentials = new NetworkCredential(_smtpConfiguration.Usuario, _smtpConfiguration.Password),
                EnableSsl = _smtpConfiguration.UsarSsl,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpConfiguration.Usuario, _smtpConfiguration.Remitente),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            var sendTask = smtpClient.SendMailAsync(mailMessage);
            await sendTask;

            if (sendTask.IsCompletedSuccessfully)
                _logger.LogInformation("Correo enviado correctamente a {email}", email);
            else
                _logger.LogError("Error enviando correo a {email}", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enviando email a {email} {message}", email, ex.Message);
        }
    }
}
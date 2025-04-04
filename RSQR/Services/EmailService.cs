using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using RSQR.Models; // Asegúrate de usar el namespace correcto

namespace RSQR.Services
{
    /// <summary>
    /// Servicio para el envío de correos electrónicos utilizando SMTP a través de MailKit.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de correo electrónico.
        /// </summary>
        /// <param name="emailSettings">Configuración de correo electrónico inyectada mediante IOptions.</param>
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        /// <summary>
        /// Envía un correo electrónico de forma asíncrona.
        /// </summary>
        /// <param name="toEmail">Dirección de correo electrónico del destinatario.</param>
        /// <param name="subject">Asunto del correo electrónico.</param>
        /// <param name="body">Cuerpo del mensaje en formato HTML.</param>
        /// <returns>Una tarea que representa la operación asíncrona de envío de correo.</returns>
        /// <exception cref="System.Exception">
        /// Se puede lanzar cuando ocurre un error durante la conexión SMTP o el envío del correo.
        /// </exception>
        /// <remarks>
        /// Este método establece una conexión SMTP con el servidor configurado, envía el correo
        /// y luego cierra la conexión. Utiliza SecureSocketOptions.Auto para negociar automáticamente
        /// la seguridad de la conexión (SSL/TLS).
        /// </remarks>
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.FromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();

            // Conectar al servidor SMTP sin autenticación
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.Auto);

            // Enviar el correo
            await smtp.SendAsync(email);

            // Desconectar
            await smtp.DisconnectAsync(true);
        }
    }
}
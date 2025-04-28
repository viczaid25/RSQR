namespace RSQR.Services
{
    /// <summary>
    /// Interfaz que define el contrato para el servicio de envío de correos electrónicos.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Envía un correo electrónico de forma asíncrona.
        /// </summary>
        /// <param name="toEmail">Dirección de correo electrónico del destinatario.</param>
        /// <param name="subject">Asunto del mensaje.</param>
        /// <param name="body">Contenido del mensaje en formato HTML.</param>
        /// <returns>Tarea que representa la operación asíncrona.</returns>
        /// <remarks>
        /// Las implementaciones de esta interfaz deben proporcionar la lógica concreta
        /// para enviar correos electrónicos utilizando algún proveedor de servicios SMTP.
        /// </remarks>
        Task SendEmailAsync(string toEmail, string subject, string body, string? bcc = null);
    }
}
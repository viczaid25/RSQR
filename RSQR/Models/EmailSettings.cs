namespace RSQR.Models
{
    /// <summary>
    /// Clase de configuración para los parámetros de envío de correos electrónicos.
    /// </summary>
    /// <remarks>
    /// Esta clase se utiliza normalmente para ser inyectada mediante IOptions&lt;EmailSettings&gt;
    /// y configurada desde appsettings.json.
    /// </remarks>
    public class EmailSettings
    {
        /// <summary>
        /// Dirección del servidor SMTP para el envío de correos.
        /// </summary>
        /// <example>smtp.midominio.com</example>
        public string? SmtpServer { get; set; }

        /// <summary>
        /// Puerto del servidor SMTP.
        /// </summary>
        /// <example>587</example>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Dirección de correo electrónico que aparecerá como remitente.
        /// </summary>
        /// <example>notificaciones@midominio.com</example>
        public string? FromEmail { get; set; }
    }
}
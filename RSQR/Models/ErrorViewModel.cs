namespace RSQR.Models
{
    /// <summary>
    /// Modelo de vista para representar información de errores en la aplicación.
    /// </summary>
    /// <remarks>
    /// Este modelo se utiliza típicamente en páginas de error para mostrar detalles
    /// sobre la solicitud fallida.
    /// </remarks>
    public class ErrorViewModel
    {
        /// <summary>
        /// Obtiene o establece el ID único de la solicitud que generó el error.
        /// </summary>
        /// <remarks>
        /// Puede ser nulo si no hay un ID de solicitud disponible.
        /// Este valor se usa comúnmente para rastrear errores en los logs.
        /// </remarks>
        public string? RequestId { get; set; }

        /// <summary>
        /// Indica si se debe mostrar el ID de solicitud en la vista.
        /// </summary>
        /// <value>
        /// <c>true</c> si el RequestId no es nulo ni vacío; de lo contrario, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Esta propiedad calculada se usa para controlar condicionalmente la visualización
        /// del ID de solicitud en las vistas de error.
        /// </remarks>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
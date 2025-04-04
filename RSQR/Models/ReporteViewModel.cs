using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace RSQR.Models
{
    /// <summary>
    /// Modelo de vista para el formulario de creación/edición de reportes.
    /// </summary>
    /// <remarks>
    /// Este ViewModel combina los datos del reporte con listas de selección
    /// necesarias para los dropdowns en la vista.
    /// </remarks>
    public class ReporteViewModel
    {
        /// <summary>
        /// Obtiene o establece el modelo de datos del reporte.
        /// </summary>
        /// <remarks>
        /// Contiene todos los campos y propiedades del reporte de calidad.
        /// </remarks>
        public Reporte Reporte { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de opciones para el campo ProblemRank.
        /// </summary>
        /// <remarks>
        /// Lista predefinida de niveles de prioridad/severidad para selección en dropdown.
        /// Incluye una opción por defecto "-Seleccionar-".
        /// </remarks>
        public List<SelectListItem> ProblemRankList { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia del ViewModel con valores por defecto.
        /// </summary>
        /// <remarks>
        /// Constructor que inicializa la lista ProblemRankList con las opciones:
        /// -Seleccionar- (valor por defecto), Bajo, Medio y Alto.
        /// </remarks>
        public ReporteViewModel()
        {
            ProblemRankList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-Seleccionar-", Selected = true },  // Opción por defecto
                new SelectListItem { Value = "Bajo", Text = "Bajo" },
                new SelectListItem { Value = "Medio", Text = "Medio" },
                new SelectListItem { Value = "Alto", Text = "Alto" }
            };
        }
    }
}
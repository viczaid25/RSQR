using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace RSQR.Models
{
    public class ReporteViewModel
    {
        public Reporte Reporte { get; set; }
        public List<SelectListItem> ProblemRankList { get; set; }

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

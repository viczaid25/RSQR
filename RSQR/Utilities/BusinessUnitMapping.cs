using System;
using System.Collections.Generic;

namespace RSQR.Utilities
{
    public static class BusinessUnitMapping
    {
        // Diccionario que mapea las unidades de negocio a sus códigos abreviados
        private static readonly Dictionary<string, string> UnitToCodeMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["STARTER"] = "ST",
            ["ALTERNATOR"] = "ALT",
            ["EPS (3G)"] = "EPS3",
            ["PT EPS"] = "PEPS",
            ["PT SSU"] = "PSSU",
            ["PT FOB"] = "PFOB",
            ["PT RCV"] = "PRCV",
            ["PT BCM"] = "PBCM",
            ["PT LFU"] = "PLFU",
            ["EPS"] = "EPS",
            ["CM"] = "CM",
            ["LCM"] = "LCM",
            ["AMP"] = "AMP",
            ["R1"] = "R1",
            ["CID"] = "CID",
            ["PT CM"] = "PCM"
        };

        /// <summary>
        /// Obtiene el código abreviado de una unidad de negocio.
        /// </summary>
        /// <param name="unitName">Nombre de la unidad de negocio (ej. "STARTER").</param>
        /// <returns>Código abreviado (ej. "ST"). Si no existe, devuelve las primeras 4 letras en mayúsculas.</returns>
        public static string GetBusinessUnitCode(string unitName)
        {
            if (string.IsNullOrWhiteSpace(unitName))
                return "GEN"; // Código por defecto si el nombre está vacío

            // Eliminar espacios y buscar en el diccionario (ignorando mayúsculas/minúsculas)
            string trimmedName = unitName.Trim();

            if (UnitToCodeMap.TryGetValue(trimmedName, out string code))
                return code;

            // Si no se encuentra, devolver las primeras 4 letras (o menos si el nombre es corto)
            return trimmedName.Length > 4
                ? trimmedName.Substring(0, 4).ToUpper()
                : trimmedName.ToUpper();
        }

        // Utilities/BusinessUnitMapping.cs
        public static string GetNombreCompleto(string codigo)
        {
            // Diccionario inverso (código -> nombre completo)
            var inverseMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["ST"] = "STARTER",
                ["ALT"] = "ALTERNATOR",
                // Agrega todos los demás mapeos inversos
            };

            return inverseMap.TryGetValue(codigo, out var nombre) ? nombre : codigo;
        }

        /// <summary>
        /// Verifica si una unidad de negocio existe en el mapeo.
        /// </summary>
        public static bool IsValidBusinessUnit(string unitName)
        {
            return UnitToCodeMap.ContainsKey(unitName?.Trim() ?? "");
        }

        /// <summary>
        /// Devuelve todos los mapeos disponibles (solo lectura).
        /// </summary>
        public static IReadOnlyDictionary<string, string> GetAllMappings()
        {
            return UnitToCodeMap;
        }
    }
}

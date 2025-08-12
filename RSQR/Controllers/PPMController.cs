using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient; // Este es el correcto para .NET Core/5+

/// <summary>
/// Controlador encargado de manejar las operaciones de cálculo de PPM (Parts Per Million).
/// Se conecta a bases de datos Oracle y SQL Server para obtener información de embarques y reclamaciones.
/// Requiere autenticación mediante el atributo <see cref="AuthorizeAttribute"/>.
/// </summary>
/// <remarks>
/// Este controlador realiza cálculos de PPM en base a:
/// - Cantidad de piezas enviadas (Oracle).
/// - Cantidad de piezas defectuosas (SQL Server).
/// 
/// Además, soporta el manejo de grupos de descripciones predefinidas y casos especiales.
/// </remarks>
[Authorize]
public class PPMController : Controller
{
    /// <summary>
    /// Configuración de la aplicación para acceder a las cadenas de conexión.
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Cadena de conexión para la base de datos Oracle.
    /// </summary>
    private readonly string oracleConnectionString;

    /// <summary>
    /// Cadena de conexión para la base de datos SQL Server.
    /// </summary>
    private readonly string sqlServerConnectionString;

    /// <summary>
    /// Diccionario que mapea nombres de grupos a una lista de descripciones que los componen.
    /// Utilizado para realizar consultas de forma agrupada.
    /// </summary>
    /// <remarks>
    /// Ejemplos:
    /// - "CM" incluye varios tipos de sistemas de control de motor.
    /// - "EPS(3G)" agrupa piezas de dirección eléctrica de tercera generación.
    /// </remarks>
    private readonly Dictionary<string, List<string>> _gruposDescripciones = new()
    {
        ["CM"] = new List<string> {
            "2.5-VCT",
            "2G-VCT",
            "3G-VCT GDI",
            "3G-VCT MERVERIC GTDI",
            "4G-VCT MPC",
            "FORD 4G-VVT MPC",
            "OCV",
            "OP-OCV",
            "VALUE VVT-ACT",
            "VALUE VVT-OCV",
            "VVT-ACV",
            "VVT-OCV"
        },
        ["EPS(3G)"] = new List<string> { 
            "EPS ECU", 
            "EPS MOTOR", 
            "EPS MCU", 
            "EPS-MCU" 
        },
        ["PT BCM"] = new List<string> {
            "C-BCM",
            "BCM V 2.0",
            "BCM V 3.0",
            "BCM VE3.0",
            "BCM VE 2.0",
            "BCM 2nd for MMVO",
            "BCM 2nd for MTMUS"
        },
        ["PT LFU"] = new List<string> {
            "LFU",
            "LFU VE2.0 for MMVO",
            "LFU VE3.0 for MTMUS",
            "LFU VE4.0 for MMVO",
            "LFU VE4.0 for MTMUS"
        },
        ["EPS"] = new List<string> {
            "EPS MCU (ILX 21M)",
            "EPS MCU (CIVIC 21M)",
            "EPS MCU (CIVIC 24.5M)",
            "EPS MCU (CIVIC 24.5M)",
            "EPS MCU (CIVIC SI)",
            "EPS MCU (HRV)",
            "EPS MCU (22M HRV F4)",
            "EPS MCU (22.5 CRV F4)",
            "EPS MCU (CLARITY)",
            "EPS MCU (25M CRV F4)",
            "EPS MCU (HRV 24.5)",
            "EPS MCU (HRV 26M)"
        },
        ["CID"] = new List<string> {
            "DU-7HU2VZL00-X",
            "DU-7HU5HVL00-X"
        },
        ["PT DISPLAY"] = new List<string> {
            "Display Cluster LHD/Entry plus",
            "Display Cluster RHD/Entry",
            "DISPLAY CLUSTER, LHD/Entry",
            "DISPLAY CLUSTER, LHD/Entry plu",
            "DISPLAY CLUSTER, RHD/Entry",
            "DISPLAY CLUSTER, RHD/Entry plu"
        }
    };

    /// <summary>
    /// Constructor del controlador que inicializa las cadenas de conexión.
    /// </summary>
    /// <param name="configuration">Interfaz de configuración para acceder a appsettings.json</param>
    public PPMController(IConfiguration configuration)
    {
        _configuration = configuration;
        oracleConnectionString = _configuration.GetConnectionString("OracleConnection");
        sqlServerConnectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    /// <summary>
    /// Acción que devuelve la vista principal del módulo PPM.
    /// </summary>
    /// <returns>Vista PPM</returns>
    public IActionResult Ppm()
    {
        return View();
    }

    /// <summary>
    /// Calcula el total de cajas enviadas y el valor PPM (Parts Per Million) para un rango de fechas y descripción especificados.
    /// </summary>
    /// <param name="fechaInicio">Fecha inicial en formato <c>yyyy-MM-dd</c>.</param>
    /// <param name="fechaFin">Fecha final en formato <c>yyyy-MM-dd</c>.</param>
    /// <param name="descripcion">Descripción del producto o grupo de productos.</param>
    /// <returns>
    /// Objeto JSON con:
    /// - <c>totalCajas</c>: total de cajas enviadas.
    /// - <c>division</c>: valor PPM calculado.
    /// - <c>error</c>: mensaje de error si aplica.
    /// </returns>
    /// <remarks>
    /// Fórmula de cálculo: <c>(Piezas defectuosas / Total de piezas enviadas) * 1,000,000</c>.
    /// </remarks>
    public IActionResult SumarCajas(string fechaInicio, string fechaFin, string descripcion)
    {
        decimal totalCajas = 0;
        decimal sumaCuantosP = 0;
        decimal division = 0;

        if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin) || string.IsNullOrEmpty(descripcion))
        {
            return Json(new { totalCajas = 0, division = 0, error = "Fechas o descripción no válidas." });
        }

        try
        {
            DateTime inicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime fin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            string inicioFormateado = inicio.ToString("dd-MMM-yy", CultureInfo.InvariantCulture).ToUpper();
            string finFormateado = fin.ToString("dd-MMM-yy", CultureInfo.InvariantCulture).ToUpper();

            // Consulta Oracle modificada para manejar grupos
            totalCajas = ObtenerTotalCajasOracle(descripcion, inicioFormateado, finFormateado);

            // Consulta SQL Server
            sumaCuantosP = ObtenerSumaCuantosPSqlServer(descripcion);

            // Cálculo PPM
            if (sumaCuantosP != 0)
            {
                division = (sumaCuantosP / totalCajas) * 1000000;
            }
            else
            {
                return Json(new { totalCajas = totalCajas, division = 0, error = "No se puede dividir por cero." });
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return Json(new { totalCajas = 0, division = 0, error = "Error: " + ex.Message });
        }

        return Json(new { totalCajas = totalCajas, division = division });
    }

    /// <summary>
    /// Obtiene datos mes a mes para generar un gráfico de PPM y reclamaciones en un rango de fechas.
    /// </summary>
    /// <param name="fechaInicio">Fecha inicial en formato <c>yyyy-MM-dd</c>.</param>
    /// <param name="fechaFin">Fecha final en formato <c>yyyy-MM-dd</c>.</param>
    /// <param name="descripcion">Descripción del producto o grupo.</param>
    /// <returns>
    /// Objeto JSON con:
    /// - <c>labels</c>: lista de meses.
    /// - <c>reclamos</c>: lista con el total de reclamaciones por mes.
    /// - <c>ppm</c>: lista con los valores PPM por mes.
    /// </returns>
    [HttpGet]
    public IActionResult ObtenerDatosGrafico(string fechaInicio, string fechaFin, string descripcion)
    {
        try
        {
            DateTime inicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime fin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var meses = new List<string>();
            var reclamosPorMes = new List<decimal>();
            var ppmPorMes = new List<decimal>();

            // Iterar por cada mes en el rango
            for (var dt = inicio; dt <= fin; dt = dt.AddMonths(1))
            {
                var mesInicio = new DateTime(dt.Year, dt.Month, 1);
                var mesFin = mesInicio.AddMonths(1).AddDays(-1);

                // Ajustar fechas para no exceder el rango solicitado
                if (mesInicio < inicio) mesInicio = inicio;
                if (mesFin > fin) mesFin = fin;

                string mesInicioFormateado = mesInicio.ToString("dd-MMM-yy", CultureInfo.InvariantCulture).ToUpper();
                string mesFinFormateado = mesFin.ToString("dd-MMM-yy", CultureInfo.InvariantCulture).ToUpper();

                // Obtener datos para el mes
                decimal totalCajas = ObtenerTotalCajasOracle(descripcion, mesInicioFormateado, mesFinFormateado);
                decimal sumaCuantosP = ObtenerSumaCuantosPSqlServer(descripcion);
                decimal ppm = sumaCuantosP != 0 ? (sumaCuantosP / totalCajas) * 1000000 : 0;

                meses.Add(mesInicio.ToString("MMM yyyy"));
                reclamosPorMes.Add(sumaCuantosP);
                ppmPorMes.Add(ppm);
            }

            return Json(new
            {
                labels = meses,
                reclamos = reclamosPorMes,
                ppm = ppmPorMes
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al obtener datos para gráfico: {ex.Message}");
            return Json(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene el PPM acumulado para el mes actual considerando todas las descripciones configuradas.
    /// </summary>
    /// <returns>Objeto JSON con el valor PPM y la fecha de referencia.</returns>
    [HttpGet]
    public IActionResult GetPpmMesActual()
    {
        try
        {
            DateTime ahora = DateTime.Now;
            DateTime inicioMes = new DateTime(ahora.Year, ahora.Month, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

            string inicioFormateado = inicioMes.ToString("dd-MMM-yy", CultureInfo.InvariantCulture).ToUpper();
            string finFormateado = finMes.ToString("dd-MMM-yy", CultureInfo.InvariantCulture).ToUpper();

            // Obtener datos para todas las descripciones
            var descripciones = new List<string> {
            "STARTER", "ALTERNATOR", "SSU Circuit Board", "FOB", "RCV", "CM",
            "EPS(3G)", "PT BCM", "PT LFU", "EPS", "CID", "LCM", "AMP",
            "R1", "PT CM", "PT DISPLAY"
        };

            decimal totalCajas = 0;
            decimal totalReclamos = 0;

            foreach (var desc in descripciones)
            {
                totalCajas += ObtenerTotalCajasOracle(desc, inicioFormateado, finFormateado);
                totalReclamos += ObtenerSumaCuantosPSqlServer(desc);
            }

            decimal ppm = totalReclamos != 0 ? (totalReclamos / totalCajas) * 1000000 : 0;

            return Json(new { ppm = ppm.ToString("N2"), fechaInicio = inicioMes.ToString("MMM yyyy") });
        }
        catch (Exception ex)
        {
            return Json(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene el PPM acumulado para el año fiscal en curso.
    /// </summary>
    /// <remarks>
    /// El año fiscal se considera desde el 1 de abril hasta el 31 de marzo.
    /// Si la fecha actual es anterior al fin del año fiscal, se usa la fecha actual como límite.
    /// </remarks>
    /// <returns>Objeto JSON con el PPM y el rango de fechas del año fiscal.</returns>
    [HttpGet]
    public IActionResult GetPpmAnioFiscal()
    {
        try
        {
            DateTime ahora = DateTime.Now;
            int anioFiscalInicio = ahora.Month >= 4 ? ahora.Year : ahora.Year - 1;
            DateTime inicioAnioFiscal = new DateTime(anioFiscalInicio, 4, 1);
            DateTime finAnioFiscal = new DateTime(anioFiscalInicio + 1, 3, 31);

            // Si el año fiscal futuro no ha terminado aún, usar la fecha actual
            if (finAnioFiscal > ahora)
            {
                finAnioFiscal = ahora;
            }

            string inicioFormateado = inicioAnioFiscal.ToString("dd-MMM-yy", CultureInfo.InvariantCulture).ToUpper();
            string finFormateado = finAnioFiscal.ToString("dd-MMM-yy", CultureInfo.InvariantCulture).ToUpper();

            // Obtener datos para todas las descripciones
            var descripciones = new List<string> {
            "STARTER", "ALTERNATOR", "SSU Circuit Board", "FOB", "RCV", "CM",
            "EPS(3G)", "PT BCM", "PT LFU", "EPS", "CID", "LCM", "AMP",
            "R1", "PT CM", "PT DISPLAY"
        };

            decimal totalCajas = 0;
            decimal totalReclamos = 0;

            foreach (var desc in descripciones)
            {
                totalCajas += ObtenerTotalCajasOracle(desc, inicioFormateado, finFormateado);
                totalReclamos += ObtenerSumaCuantosPSqlServer(desc);
            }

            decimal ppm = totalReclamos != 0 ? (totalReclamos / totalCajas) * 1000000 : 0;

            return Json(new
            {
                ppm = ppm.ToString("N2"),
                fechaInicio = inicioAnioFiscal.ToString("MMM yyyy"),
                fechaFin = finAnioFiscal.ToString("MMM yyyy")
            });
        }
        catch (Exception ex)
        {
            return Json(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene el total de cajas enviadas desde Oracle para una descripción o grupo de descripciones en un rango de fechas.
    /// </summary>
    /// <param name="descripcion">Descripción o grupo.</param>
    /// <param name="fechaInicio">Fecha inicial en formato <c>dd-MMM-yy</c>.</param>
    /// <param name="fechaFin">Fecha final en formato <c>dd-MMM-yy</c>.</param>
    /// <returns>Total de cajas enviadas.</returns>
    private decimal ObtenerTotalCajasOracle(string descripcion, string fechaInicio, string fechaFin)
    {
        using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionString))
        {
            oracleConnection.Open();
            using (OracleCommand oracleCommand = new OracleCommand())
            {
                oracleCommand.Connection = oracleConnection;

                Debug.WriteLine($"Procesando descripción: {descripcion}");


                // Primero verificar si es un grupo definido
                if (_gruposDescripciones.TryGetValue(descripcion, out var descripcionesGrupo))
                {
                    Debug.WriteLine($"Identificado como grupo con {descripcionesGrupo.Count} elementos");

                    string inClause = string.Join(",", descripcionesGrupo.Select(d => $"'{d}'"));
                    oracleCommand.CommandText = $@"
                    SELECT SUM(SHP_QTY) 
                    FROM CB_SHIP_D
                    WHERE DSCRPTN IN ({inClause}) 
                    AND INVC_NO IN (
                        SELECT INVC_NO
                        FROM CB_SHIP_H
                        WHERE ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                        AND TO_DATE(:fechaFin, 'DD-MON-YY')
                    )";
                }
                // Luego verificar casos especiales individuales
                else if (descripcion == "LCM")
                {
                    Debug.WriteLine("Identificado como caso especial LCM");
                    oracleCommand.CommandText = @"
                    SELECT NVL(SUM(d.SHP_QTY), 0) 
                    FROM CB_SHIP_D d
                    INNER JOIN CB_SHIP_H h ON d.INVC_NO = h.INVC_NO
                    WHERE (d.DSCRPTN = 'LCM' 
                           OR REGEXP_LIKE(d.DSCRPTN, '^W003'))
                    AND h.ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                       AND TO_DATE(:fechaFin, 'DD-MON-YY')";

                    // Log adicional para LCM
                    Debug.WriteLine("Consulta LCM: " + oracleCommand.CommandText);
                    Debug.WriteLine("Fechas usadas: " + fechaInicio + " - " + fechaFin);
                }
                else if (descripcion == "AMP")
                {
                    Debug.WriteLine("Identificado como caso especial AMP");
                    oracleCommand.CommandText = @"
                    SELECT NVL(SUM(d.SHP_QTY), 0) 
                    FROM CB_SHIP_D d
                    INNER JOIN CB_SHIP_H h ON d.INVC_NO = h.INVC_NO
                    WHERE (d.DSCRPTN LIKE 'CV-%'
                            OR REGEXP_LIKE(d.DSCRPTN, '^CZ-'))
                    AND h.ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                       AND TO_DATE(:fechaFin, 'DD-MON-YY')";

                    // Log adicional para AMP
                    Debug.WriteLine("Consulta AMP: " + oracleCommand.CommandText);
                    Debug.WriteLine("Fechas usadas: " + fechaInicio + " - " + fechaFin);
                }
                else if (descripcion == "R1")
                {
                    Debug.WriteLine("Identificado como caso especial R1");
                    oracleCommand.CommandText = @"
                    SELECT NVL(SUM(d.SHP_QTY), 0) 
                    FROM CB_SHIP_D d
                    INNER JOIN CB_SHIP_H h ON d.INVC_NO = h.INVC_NO
                    WHERE (d.DSCRPTN LIKE 'NR%')
                    AND h.ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                       AND TO_DATE(:fechaFin, 'DD-MON-YY')";

                    // Log adicional para R1
                    Debug.WriteLine("Consulta R1: " + oracleCommand.CommandText);
                    Debug.WriteLine("Fechas usadas: " + fechaInicio + " - " + fechaFin);
                }
                else if (descripcion == "PT CM")
                {
                    Debug.WriteLine("Identificado como caso especial PT CM");
                    oracleCommand.CommandText = @"
                    SELECT SUM(SHP_QTY) 
                    FROM CB_SHIP_D
                    WHERE DSCRPTN = 'VCT DRAGON'
                    AND INVC_NO IN (
                        SELECT INVC_NO
                        FROM CB_SHIP_H
                        WHERE ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                        AND TO_DATE(:fechaFin, 'DD-MON-YY')
                    )";
                }
                else if (descripcion == "SSU Circuit Board")
                {
                    Debug.WriteLine("Identificado como caso especial SSU Circuit Board");
                    oracleCommand.CommandText = @"
                    SELECT SUM(SHP_QTY) 
                    FROM CB_SHIP_D
                    WHERE DSCRPTN = 'SSU Circuit Board'
                    AND INVC_NO IN (
                        SELECT INVC_NO
                        FROM CB_SHIP_H
                        WHERE ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                        AND TO_DATE(:fechaFin, 'DD-MON-YY')
                    )";
                }
                else if (descripcion == "FOB")
                {
                    Debug.WriteLine("Identificado como caso especial FOB");
                    oracleCommand.CommandText = @"
                    SELECT SUM(SHP_QTY) 
                    FROM CB_SHIP_D
                    WHERE DSCRPTN = 'FOB'
                    AND INVC_NO IN (
                        SELECT INVC_NO
                        FROM CB_SHIP_H
                        WHERE ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                        AND TO_DATE(:fechaFin, 'DD-MON-YY')
                    )";
                }
                else if (descripcion == "RCV")
                {
                    Debug.WriteLine("Identificado como caso especial RCV");
                    oracleCommand.CommandText = @"
                    SELECT SUM(SHP_QTY) 
                    FROM CB_SHIP_D
                    WHERE DSCRPTN = 'RCV'
                    AND INVC_NO IN (
                        SELECT INVC_NO
                        FROM CB_SHIP_H
                        WHERE ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                        AND TO_DATE(:fechaFin, 'DD-MON-YY')
                    )";
                }
                else
                {
                    Debug.WriteLine("Identificado como descripción individual");
                    oracleCommand.CommandText = @"
                    SELECT SUM(SHP_QTY) 
                    FROM CB_SHIP_D
                    WHERE DSCRPTN = :descripcion 
                    AND INVC_NO IN (
                        SELECT INVC_NO
                        FROM CB_SHIP_H
                        WHERE ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                        AND TO_DATE(:fechaFin, 'DD-MON-YY')
                    )";
                    oracleCommand.Parameters.Add(new OracleParameter("descripcion", descripcion));
                }

                oracleCommand.Parameters.Add(new OracleParameter("fechaInicio", fechaInicio));
                oracleCommand.Parameters.Add(new OracleParameter("fechaFin", fechaFin));

                Debug.WriteLine($"Consulta Oracle: {oracleCommand.CommandText}");

                object result = oracleCommand.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }
    }

    /// <summary>
    /// Obtiene la cantidad de piezas defectuosas desde SQL Server para una descripción.
    /// </summary>
    /// <param name="descripcion">Descripción del producto o grupo.</param>
    /// <returns>Total de piezas defectuosas.</returns>
    private decimal ObtenerSumaCuantosPSqlServer(string descripcion)
    {
        using (SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString))
        {
            sqlConnection.Open();
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = @"
                    SELECT SUM(CuantosP) 
                    FROM [dbo].[qcPpmReport]
                    WHERE Linea = @descripcion";

                sqlCommand.Parameters.Add(new SqlParameter("@descripcion", descripcion));

                Debug.WriteLine($"Consulta SQL Server: {sqlCommand.CommandText}");

                object result = sqlCommand.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }
    }
}
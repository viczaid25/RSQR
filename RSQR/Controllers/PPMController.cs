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
/// Controlador para manejar las operaciones relacionadas con el cálculo PPM (Parts Per Million).
/// Requiere autenticación para acceder a sus métodos.
/// </summary>
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
    /// Diccionario que mapea grupos de descripciones para consultas agrupadas.
    /// Contiene tres grupos principales:
    /// - CM_GROUP: Descripciones relacionadas con sistemas de control de motor
    /// - EPS_GROUP: Descripciones relacionadas con sistemas de dirección eléctrica
    /// - BCM_GROUP: Descripciones relacionadas con módulos de control de carrocería
    /// </summary>
    private readonly Dictionary<string, List<string>> _gruposDescripciones = new()
    {
        ["CM"] = new List<string> {
            "FORD 4G-VVT MPC",
            "4G-VCT MPC",
            "3G-VCT MERVERIC GTDI",
            "2G-VCT",
            "2.5-VCT",
            "OCV",
            "VVT-OCV",
            "VVT-ACV",
            "VALUE VVT-ACT",
            "OP-OCV"
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
            "BCM 2nd for MMVO",
            "BCM 2nd for MTMUS"
        },
        ["PT LFU"] = new List<string> {
            "LFU",
            "LFU 2nd for MMVO",
            "LFU 2nd for MTMUS",
            "LFU VE 2.0 for MMVO",
            "LFU VE 2.0 for MTMUS "
        },
        ["EPS"] = new List<string> {
            "EPS MCU (ILX)",
            "EPS MCU (HRV)",
            "EPS MCU (CIVIC)",
            "EPS MCU (CRV)"
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
    /// Calcula el total de cajas y el valor PPM para un rango de fechas y descripción específicos.
    /// </summary>
    /// <param name="fechaInicio">Fecha de inicio en formato yyyy-MM-dd</param>
    /// <param name="fechaFin">Fecha de fin en formato yyyy-MM-dd</param>
    /// <param name="descripcion">Descripción del producto o grupo de productos</param>
    /// <returns>Objeto JSON con total de cajas, valor PPM y posibles errores</returns>
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
    /// Obtiene el total de cajas enviadas desde Oracle para un producto o grupo de productos
    /// en un rango de fechas específico.
    /// </summary>
    /// <param name="descripcion">Descripción del producto o nombre del grupo</param>
    /// <param name="fechaInicio">Fecha de inicio formateada (dd-MMM-yy)</param>
    /// <param name="fechaFin">Fecha de fin formateada (dd-MMM-yy)</param>
    /// <returns>Total de cajas como valor decimal</returns>
    private decimal ObtenerTotalCajasOracle(string descripcion, string fechaInicio, string fechaFin)
    {
        using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionString))
        {
            oracleConnection.Open();
            using (OracleCommand oracleCommand = new OracleCommand())
            {
                oracleCommand.Connection = oracleConnection;

                // Construir consulta según si es un grupo o descripción individual
                if (_gruposDescripciones.TryGetValue(descripcion, out var descripcionesGrupo))
                {
                    // Consulta para grupos
                    string inClause = string.Join(",", descripcionesGrupo.Select(d => $"'{d}'"));
                    oracleCommand.CommandText = $@"
                        SELECT SUM(QTY_BOX) 
                        FROM CB_SHIP_D
                        WHERE DSCRPTN IN ({inClause}) 
                        AND INVC_NO IN (
                            SELECT INVC_NO
                            FROM CB_SHIP_H
                            WHERE ETD_DATE BETWEEN TO_DATE(:fechaInicio, 'DD-MON-YY') 
                            AND TO_DATE(:fechaFin, 'DD-MON-YY')
                        )";
                }
                else
                {
                    // Consulta para descripción individual
                    oracleCommand.CommandText = @"
                        SELECT SUM(QTY_BOX) 
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
    /// Obtiene la suma de partes defectuosas desde SQL Server para una descripción específica.
    /// </summary>
    /// <param name="descripcion">Descripción del producto o nombre del grupo</param>
    /// <returns>Suma de partes defectuosas como valor decimal</returns>
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
                    FROM PpmReports
                    WHERE Linea = @descripcion";

                sqlCommand.Parameters.Add(new SqlParameter("@descripcion", descripcion));

                Debug.WriteLine($"Consulta SQL Server: {sqlCommand.CommandText}");

                object result = sqlCommand.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }
    }
}
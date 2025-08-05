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
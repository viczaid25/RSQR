<a name='assembly'></a>
# RSQR

## Contents

- [AccountController](#T-RSQR-Controllers-AccountController 'RSQR.Controllers.AccountController')
  - [#ctor(adService)](#M-RSQR-Controllers-AccountController-#ctor-RSQR-Services-ActiveDirectoryService- 'RSQR.Controllers.AccountController.#ctor(RSQR.Services.ActiveDirectoryService)')
  - [Login()](#M-RSQR-Controllers-AccountController-Login 'RSQR.Controllers.AccountController.Login')
  - [Login(username,password)](#M-RSQR-Controllers-AccountController-Login-System-String,System-String- 'RSQR.Controllers.AccountController.Login(System.String,System.String)')
  - [Logout()](#M-RSQR-Controllers-AccountController-Logout 'RSQR.Controllers.AccountController.Logout')
- [ActiveDirectoryService](#T-RSQR-Services-ActiveDirectoryService 'RSQR.Services.ActiveDirectoryService')
  - [#ctor(domain)](#M-RSQR-Services-ActiveDirectoryService-#ctor-System-String- 'RSQR.Services.ActiveDirectoryService.#ctor(System.String)')
  - [GetDisplayName(username,password)](#M-RSQR-Services-ActiveDirectoryService-GetDisplayName-System-String,System-String- 'RSQR.Services.ActiveDirectoryService.GetDisplayName(System.String,System.String)')
  - [ValidateCredentials(username,password)](#M-RSQR-Services-ActiveDirectoryService-ValidateCredentials-System-String,System-String- 'RSQR.Services.ActiveDirectoryService.ValidateCredentials(System.String,System.String)')
- [AdUserManagerService](#T-AdUserManagerService 'AdUserManagerService')
  - [#ctor(userManager)](#M-AdUserManagerService-#ctor-Microsoft-AspNetCore-Identity-UserManager{Microsoft-AspNetCore-Identity-IdentityUser}- 'AdUserManagerService.#ctor(Microsoft.AspNetCore.Identity.UserManager{Microsoft.AspNetCore.Identity.IdentityUser})')
  - [SyncAdUserAsync(principal)](#M-AdUserManagerService-SyncAdUserAsync-System-Security-Claims-ClaimsPrincipal- 'AdUserManagerService.SyncAdUserAsync(System.Security.Claims.ClaimsPrincipal)')
- [AdUserSyncMiddleware](#T-AdUserSyncMiddleware 'AdUserSyncMiddleware')
  - [#ctor(next)](#M-AdUserSyncMiddleware-#ctor-Microsoft-AspNetCore-Http-RequestDelegate- 'AdUserSyncMiddleware.#ctor(Microsoft.AspNetCore.Http.RequestDelegate)')
  - [InvokeAsync(context,adUserManagerService)](#M-AdUserSyncMiddleware-InvokeAsync-Microsoft-AspNetCore-Http-HttpContext,AdUserManagerService- 'AdUserSyncMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext,AdUserManagerService)')
- [ApplicationDbContext](#T-RSQR-Data-ApplicationDbContext 'RSQR.Data.ApplicationDbContext')
  - [#ctor(options)](#M-RSQR-Data-ApplicationDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RSQR-Data-ApplicationDbContext}- 'RSQR.Data.ApplicationDbContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{RSQR.Data.ApplicationDbContext})')
  - [ConsecutivosArchivos](#P-RSQR-Data-ApplicationDbContext-ConsecutivosArchivos 'RSQR.Data.ApplicationDbContext.ConsecutivosArchivos')
  - [PpmReports](#P-RSQR-Data-ApplicationDbContext-PpmReports 'RSQR.Data.ApplicationDbContext.PpmReports')
  - [Reportes](#P-RSQR-Data-ApplicationDbContext-Reportes 'RSQR.Data.ApplicationDbContext.Reportes')
  - [OnModelCreating(modelBuilder)](#M-RSQR-Data-ApplicationDbContext-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Data.ApplicationDbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)')
- [BusinessUnitMapping](#T-RSQR-Utilities-BusinessUnitMapping 'RSQR.Utilities.BusinessUnitMapping')
  - [GetAllMappings()](#M-RSQR-Utilities-BusinessUnitMapping-GetAllMappings 'RSQR.Utilities.BusinessUnitMapping.GetAllMappings')
  - [GetBusinessUnitCode(unitName)](#M-RSQR-Utilities-BusinessUnitMapping-GetBusinessUnitCode-System-String- 'RSQR.Utilities.BusinessUnitMapping.GetBusinessUnitCode(System.String)')
  - [IsValidBusinessUnit()](#M-RSQR-Utilities-BusinessUnitMapping-IsValidBusinessUnit-System-String- 'RSQR.Utilities.BusinessUnitMapping.IsValidBusinessUnit(System.String)')
- [CincoMOpciones](#T-RSQR-Models-CincoMOpciones 'RSQR.Models.CincoMOpciones')
  - [ManoDeObra](#F-RSQR-Models-CincoMOpciones-ManoDeObra 'RSQR.Models.CincoMOpciones.ManoDeObra')
  - [Maquina](#F-RSQR-Models-CincoMOpciones-Maquina 'RSQR.Models.CincoMOpciones.Maquina')
  - [Material](#F-RSQR-Models-CincoMOpciones-Material 'RSQR.Models.CincoMOpciones.Material')
  - [Medicion](#F-RSQR-Models-CincoMOpciones-Medicion 'RSQR.Models.CincoMOpciones.Medicion')
  - [MedioAmbiente](#F-RSQR-Models-CincoMOpciones-MedioAmbiente 'RSQR.Models.CincoMOpciones.MedioAmbiente')
- [ConsecutivoArchivo](#T-RSQR-Models-ConsecutivoArchivo 'RSQR.Models.ConsecutivoArchivo')
  - [Anio](#P-RSQR-Models-ConsecutivoArchivo-Anio 'RSQR.Models.ConsecutivoArchivo.Anio')
  - [CodigoNegocio](#P-RSQR-Models-ConsecutivoArchivo-CodigoNegocio 'RSQR.Models.ConsecutivoArchivo.CodigoNegocio')
  - [Consecutivo](#P-RSQR-Models-ConsecutivoArchivo-Consecutivo 'RSQR.Models.ConsecutivoArchivo.Consecutivo')
  - [FechaActualizacion](#P-RSQR-Models-ConsecutivoArchivo-FechaActualizacion 'RSQR.Models.ConsecutivoArchivo.FechaActualizacion')
  - [Id](#P-RSQR-Models-ConsecutivoArchivo-Id 'RSQR.Models.ConsecutivoArchivo.Id')
  - [UnidadNegocio](#P-RSQR-Models-ConsecutivoArchivo-UnidadNegocio 'RSQR.Models.ConsecutivoArchivo.UnidadNegocio')
- [EmailService](#T-RSQR-Services-EmailService 'RSQR.Services.EmailService')
  - [#ctor(emailSettings)](#M-RSQR-Services-EmailService-#ctor-Microsoft-Extensions-Options-IOptions{RSQR-Models-EmailSettings}- 'RSQR.Services.EmailService.#ctor(Microsoft.Extensions.Options.IOptions{RSQR.Models.EmailSettings})')
  - [SendEmailAsync(toEmail,subject,body)](#M-RSQR-Services-EmailService-SendEmailAsync-System-String,System-String,System-String,System-String- 'RSQR.Services.EmailService.SendEmailAsync(System.String,System.String,System.String,System.String)')
- [EmailSettings](#T-RSQR-Models-EmailSettings 'RSQR.Models.EmailSettings')
  - [FromEmail](#P-RSQR-Models-EmailSettings-FromEmail 'RSQR.Models.EmailSettings.FromEmail')
  - [SmtpPort](#P-RSQR-Models-EmailSettings-SmtpPort 'RSQR.Models.EmailSettings.SmtpPort')
  - [SmtpServer](#P-RSQR-Models-EmailSettings-SmtpServer 'RSQR.Models.EmailSettings.SmtpServer')
- [ErrorViewModel](#T-RSQR-Models-ErrorViewModel 'RSQR.Models.ErrorViewModel')
  - [RequestId](#P-RSQR-Models-ErrorViewModel-RequestId 'RSQR.Models.ErrorViewModel.RequestId')
  - [ShowRequestId](#P-RSQR-Models-ErrorViewModel-ShowRequestId 'RSQR.Models.ErrorViewModel.ShowRequestId')
- [HomeController](#T-RSQR-Controllers-HomeController 'RSQR.Controllers.HomeController')
  - [#ctor(logger)](#M-RSQR-Controllers-HomeController-#ctor-Microsoft-Extensions-Logging-ILogger{RSQR-Controllers-HomeController}- 'RSQR.Controllers.HomeController.#ctor(Microsoft.Extensions.Logging.ILogger{RSQR.Controllers.HomeController})')
  - [Error()](#M-RSQR-Controllers-HomeController-Error 'RSQR.Controllers.HomeController.Error')
  - [Index()](#M-RSQR-Controllers-HomeController-Index 'RSQR.Controllers.HomeController.Index')
- [IEmailService](#T-RSQR-Services-IEmailService 'RSQR.Services.IEmailService')
  - [SendEmailAsync(toEmail,subject,body)](#M-RSQR-Services-IEmailService-SendEmailAsync-System-String,System-String,System-String,System-String- 'RSQR.Services.IEmailService.SendEmailAsync(System.String,System.String,System.String,System.String)')
- [PPMController](#T-PPMController 'PPMController')
  - [#ctor(configuration)](#M-PPMController-#ctor-Microsoft-Extensions-Configuration-IConfiguration- 'PPMController.#ctor(Microsoft.Extensions.Configuration.IConfiguration)')
  - [_configuration](#F-PPMController-_configuration 'PPMController._configuration')
  - [_gruposDescripciones](#F-PPMController-_gruposDescripciones 'PPMController._gruposDescripciones')
  - [oracleConnectionString](#F-PPMController-oracleConnectionString 'PPMController.oracleConnectionString')
  - [sqlServerConnectionString](#F-PPMController-sqlServerConnectionString 'PPMController.sqlServerConnectionString')
  - [GetPpmAnioFiscal()](#M-PPMController-GetPpmAnioFiscal 'PPMController.GetPpmAnioFiscal')
  - [GetPpmMesActual()](#M-PPMController-GetPpmMesActual 'PPMController.GetPpmMesActual')
  - [ObtenerDatosGrafico(fechaInicio,fechaFin,descripcion)](#M-PPMController-ObtenerDatosGrafico-System-String,System-String,System-String- 'PPMController.ObtenerDatosGrafico(System.String,System.String,System.String)')
  - [ObtenerSumaCuantosPSqlServer(descripcion)](#M-PPMController-ObtenerSumaCuantosPSqlServer-System-String- 'PPMController.ObtenerSumaCuantosPSqlServer(System.String)')
  - [ObtenerTotalCajasOracle(descripcion,fechaInicio,fechaFin)](#M-PPMController-ObtenerTotalCajasOracle-System-String,System-String,System-String- 'PPMController.ObtenerTotalCajasOracle(System.String,System.String,System.String)')
  - [Ppm()](#M-PPMController-Ppm 'PPMController.Ppm')
  - [SumarCajas(fechaInicio,fechaFin,descripcion)](#M-PPMController-SumarCajas-System-String,System-String,System-String- 'PPMController.SumarCajas(System.String,System.String,System.String)')
- [PpmReport](#T-RSQR-Models-PpmReport 'RSQR.Models.PpmReport')
  - [Comentarios](#P-RSQR-Models-PpmReport-Comentarios 'RSQR.Models.PpmReport.Comentarios')
  - [CuantosP](#P-RSQR-Models-PpmReport-CuantosP 'RSQR.Models.PpmReport.CuantosP')
  - [Customer](#P-RSQR-Models-PpmReport-Customer 'RSQR.Models.PpmReport.Customer')
  - [CustomerClaimNumber](#P-RSQR-Models-PpmReport-CustomerClaimNumber 'RSQR.Models.PpmReport.CustomerClaimNumber')
  - [CustomerPartNumber](#P-RSQR-Models-PpmReport-CustomerPartNumber 'RSQR.Models.PpmReport.CustomerPartNumber')
  - [DateOfClose](#P-RSQR-Models-PpmReport-DateOfClose 'RSQR.Models.PpmReport.DateOfClose')
  - [Fecha](#P-RSQR-Models-PpmReport-Fecha 'RSQR.Models.PpmReport.Fecha')
  - [Id](#P-RSQR-Models-PpmReport-Id 'RSQR.Models.PpmReport.Id')
  - [ImpactoPPM](#P-RSQR-Models-PpmReport-ImpactoPPM 'RSQR.Models.PpmReport.ImpactoPPM')
  - [InvestigationReport](#P-RSQR-Models-PpmReport-InvestigationReport 'RSQR.Models.PpmReport.InvestigationReport')
  - [Linea](#P-RSQR-Models-PpmReport-Linea 'RSQR.Models.PpmReport.Linea')
  - [Lote](#P-RSQR-Models-PpmReport-Lote 'RSQR.Models.PpmReport.Lote')
  - [Mileage](#P-RSQR-Models-PpmReport-Mileage 'RSQR.Models.PpmReport.Mileage')
  - [MotherFactory](#P-RSQR-Models-PpmReport-MotherFactory 'RSQR.Models.PpmReport.MotherFactory')
  - [NumParteAfectado](#P-RSQR-Models-PpmReport-NumParteAfectado 'RSQR.Models.PpmReport.NumParteAfectado')
  - [Responsabilidad](#P-RSQR-Models-PpmReport-Responsabilidad 'RSQR.Models.PpmReport.Responsabilidad')
  - [Tipo](#P-RSQR-Models-PpmReport-Tipo 'RSQR.Models.PpmReport.Tipo')
  - [TituloProblema](#P-RSQR-Models-PpmReport-TituloProblema 'RSQR.Models.PpmReport.TituloProblema')
- [Reporte](#T-RSQR-Models-Reporte 'RSQR.Models.Reporte')
  - [AP_NPR](#P-RSQR-Models-Reporte-AP_NPR 'RSQR.Models.Reporte.AP_NPR')
  - [AlertaCalidad](#P-RSQR-Models-Reporte-AlertaCalidad 'RSQR.Models.Reporte.AlertaCalidad')
  - [CincoM](#P-RSQR-Models-Reporte-CincoM 'RSQR.Models.Reporte.CincoM')
  - [Comentarios](#P-RSQR-Models-Reporte-Comentarios 'RSQR.Models.Reporte.Comentarios')
  - [ComoP](#P-RSQR-Models-Reporte-ComoP 'RSQR.Models.Reporte.ComoP')
  - [ContencionActivity](#P-RSQR-Models-Reporte-ContencionActivity 'RSQR.Models.Reporte.ContencionActivity')
  - [ContencionConsiderar](#P-RSQR-Models-Reporte-ContencionConsiderar 'RSQR.Models.Reporte.ContencionConsiderar')
  - [ContencionEffectiveness](#P-RSQR-Models-Reporte-ContencionEffectiveness 'RSQR.Models.Reporte.ContencionEffectiveness')
  - [ContencionItems](#P-RSQR-Models-Reporte-ContencionItems 'RSQR.Models.Reporte.ContencionItems')
  - [ContencionNgParts](#P-RSQR-Models-Reporte-ContencionNgParts 'RSQR.Models.Reporte.ContencionNgParts')
  - [ContencionOkParts](#P-RSQR-Models-Reporte-ContencionOkParts 'RSQR.Models.Reporte.ContencionOkParts')
  - [ContencionResponsable](#P-RSQR-Models-Reporte-ContencionResponsable 'RSQR.Models.Reporte.ContencionResponsable')
  - [ContencionTotalSuspeciousParts](#P-RSQR-Models-Reporte-ContencionTotalSuspeciousParts 'RSQR.Models.Reporte.ContencionTotalSuspeciousParts')
  - [ControlesEstablecidos](#P-RSQR-Models-Reporte-ControlesEstablecidos 'RSQR.Models.Reporte.ControlesEstablecidos')
  - [CuandoP](#P-RSQR-Models-Reporte-CuandoP 'RSQR.Models.Reporte.CuandoP')
  - [CuantosP](#P-RSQR-Models-Reporte-CuantosP 'RSQR.Models.Reporte.CuantosP')
  - [Customer](#P-RSQR-Models-Reporte-Customer 'RSQR.Models.Reporte.Customer')
  - [CustomerClaimDisplayNumber](#P-RSQR-Models-Reporte-CustomerClaimDisplayNumber 'RSQR.Models.Reporte.CustomerClaimDisplayNumber')
  - [CustomerClaimNumber](#P-RSQR-Models-Reporte-CustomerClaimNumber 'RSQR.Models.Reporte.CustomerClaimNumber')
  - [CustomerClaimPrefix](#P-RSQR-Models-Reporte-CustomerClaimPrefix 'RSQR.Models.Reporte.CustomerClaimPrefix')
  - [CustomerPartNumber](#P-RSQR-Models-Reporte-CustomerPartNumber 'RSQR.Models.Reporte.CustomerPartNumber')
  - [DateOfClose](#P-RSQR-Models-Reporte-DateOfClose 'RSQR.Models.Reporte.DateOfClose')
  - [Descripcion](#P-RSQR-Models-Reporte-Descripcion 'RSQR.Models.Reporte.Descripcion')
  - [DescripcionProblema](#P-RSQR-Models-Reporte-DescripcionProblema 'RSQR.Models.Reporte.DescripcionProblema')
  - [Deteccion](#P-RSQR-Models-Reporte-Deteccion 'RSQR.Models.Reporte.Deteccion')
  - [Disposicion](#P-RSQR-Models-Reporte-Disposicion 'RSQR.Models.Reporte.Disposicion')
  - [DondeP](#P-RSQR-Models-Reporte-DondeP 'RSQR.Models.Reporte.DondeP')
  - [EntrevistaInvolucrados](#P-RSQR-Models-Reporte-EntrevistaInvolucrados 'RSQR.Models.Reporte.EntrevistaInvolucrados')
  - [EvidenciaFotografica](#P-RSQR-Models-Reporte-EvidenciaFotografica 'RSQR.Models.Reporte.EvidenciaFotografica')
  - [Fecha](#P-RSQR-Models-Reporte-Fecha 'RSQR.Models.Reporte.Fecha')
  - [Id](#P-RSQR-Models-Reporte-Id 'RSQR.Models.Reporte.Id')
  - [ImpactoPPM](#P-RSQR-Models-Reporte-ImpactoPPM 'RSQR.Models.Reporte.ImpactoPPM')
  - [InvestigationReport](#P-RSQR-Models-Reporte-InvestigationReport 'RSQR.Models.Reporte.InvestigationReport')
  - [Linea](#P-RSQR-Models-Reporte-Linea 'RSQR.Models.Reporte.Linea')
  - [Lote](#P-RSQR-Models-Reporte-Lote 'RSQR.Models.Reporte.Lote')
  - [Mileage](#P-RSQR-Models-Reporte-Mileage 'RSQR.Models.Reporte.Mileage')
  - [ModoFalla](#P-RSQR-Models-Reporte-ModoFalla 'RSQR.Models.Reporte.ModoFalla')
  - [MotherFactory](#P-RSQR-Models-Reporte-MotherFactory 'RSQR.Models.Reporte.MotherFactory')
  - [NumParteAfectado](#P-RSQR-Models-Reporte-NumParteAfectado 'RSQR.Models.Reporte.NumParteAfectado')
  - [Ocurrencia](#P-RSQR-Models-Reporte-Ocurrencia 'RSQR.Models.Reporte.Ocurrencia')
  - [PorqueP](#P-RSQR-Models-Reporte-PorqueP 'RSQR.Models.Reporte.PorqueP')
  - [ProblemRank](#P-RSQR-Models-Reporte-ProblemRank 'RSQR.Models.Reporte.ProblemRank')
  - [QueP](#P-RSQR-Models-Reporte-QueP 'RSQR.Models.Reporte.QueP')
  - [QuienP](#P-RSQR-Models-Reporte-QuienP 'RSQR.Models.Reporte.QuienP')
  - [Responsabilidad](#P-RSQR-Models-Reporte-Responsabilidad 'RSQR.Models.Reporte.Responsabilidad')
  - [Severidad](#P-RSQR-Models-Reporte-Severidad 'RSQR.Models.Reporte.Severidad')
  - [Tipo](#P-RSQR-Models-Reporte-Tipo 'RSQR.Models.Reporte.Tipo')
  - [TituloProblema](#P-RSQR-Models-Reporte-TituloProblema 'RSQR.Models.Reporte.TituloProblema')
  - [UserName](#P-RSQR-Models-Reporte-UserName 'RSQR.Models.Reporte.UserName')
- [ReporteViewModel](#T-RSQR-Models-ReporteViewModel 'RSQR.Models.ReporteViewModel')
  - [#ctor()](#M-RSQR-Models-ReporteViewModel-#ctor 'RSQR.Models.ReporteViewModel.#ctor')
  - [ProblemRankList](#P-RSQR-Models-ReporteViewModel-ProblemRankList 'RSQR.Models.ReporteViewModel.ProblemRankList')
  - [Reporte](#P-RSQR-Models-ReporteViewModel-Reporte 'RSQR.Models.ReporteViewModel.Reporte')
- [ReportesController](#T-RSQR-Controllers-ReportesController 'RSQR.Controllers.ReportesController')
  - [#ctor(context)](#M-RSQR-Controllers-ReportesController-#ctor-RSQR-Data-ApplicationDbContext,RSQR-Services-IEmailService,Microsoft-Extensions-Logging-ILogger{RSQR-Controllers-ReportesController}- 'RSQR.Controllers.ReportesController.#ctor(RSQR.Data.ApplicationDbContext,RSQR.Services.IEmailService,Microsoft.Extensions.Logging.ILogger{RSQR.Controllers.ReportesController})')
  - [Create()](#M-RSQR-Controllers-ReportesController-Create 'RSQR.Controllers.ReportesController.Create')
  - [Create(reporte,EvidenciaFotografica)](#M-RSQR-Controllers-ReportesController-Create-RSQR-Models-Reporte,System-Collections-Generic-List{Microsoft-AspNetCore-Http-IFormFile}- 'RSQR.Controllers.ReportesController.Create(RSQR.Models.Reporte,System.Collections.Generic.List{Microsoft.AspNetCore.Http.IFormFile})')
  - [Delete(id)](#M-RSQR-Controllers-ReportesController-Delete-System-Nullable{System-Int32}- 'RSQR.Controllers.ReportesController.Delete(System.Nullable{System.Int32})')
  - [DeleteConfirmed(id)](#M-RSQR-Controllers-ReportesController-DeleteConfirmed-System-Int32- 'RSQR.Controllers.ReportesController.DeleteConfirmed(System.Int32)')
  - [Details(id)](#M-RSQR-Controllers-ReportesController-Details-System-Nullable{System-Int32}- 'RSQR.Controllers.ReportesController.Details(System.Nullable{System.Int32})')
  - [Edit(id)](#M-RSQR-Controllers-ReportesController-Edit-System-Nullable{System-Int32}- 'RSQR.Controllers.ReportesController.Edit(System.Nullable{System.Int32})')
  - [Edit(id,reporte)](#M-RSQR-Controllers-ReportesController-Edit-System-Int32,RSQR-Models-Reporte,System-Collections-Generic-List{Microsoft-AspNetCore-Http-IFormFile},System-Collections-Generic-List{System-Int32}- 'RSQR.Controllers.ReportesController.Edit(System.Int32,RSQR.Models.Reporte,System.Collections.Generic.List{Microsoft.AspNetCore.Http.IFormFile},System.Collections.Generic.List{System.Int32})')
  - [ExportToExcel()](#M-RSQR-Controllers-ReportesController-ExportToExcel-System-Int32,System-String- 'RSQR.Controllers.ReportesController.ExportToExcel(System.Int32,System.String)')
  - [Index()](#M-RSQR-Controllers-ReportesController-Index 'RSQR.Controllers.ReportesController.Index')
  - [ReporteExists(id)](#M-RSQR-Controllers-ReportesController-ReporteExists-System-Int32- 'RSQR.Controllers.ReportesController.ReporteExists(System.Int32)')
- [ResponsabilidadOpciones](#T-RSQR-Models-ResponsabilidadOpciones 'RSQR.Models.ResponsabilidadOpciones')
  - [Meax](#F-RSQR-Models-ResponsabilidadOpciones-Meax 'RSQR.Models.ResponsabilidadOpciones.Meax')
  - [Proveedor](#F-RSQR-Models-ResponsabilidadOpciones-Proveedor 'RSQR.Models.ResponsabilidadOpciones.Proveedor')
- [TipoReporte](#T-RSQR-Models-TipoReporte 'RSQR.Models.TipoReporte')
  - [Field](#F-RSQR-Models-TipoReporte-Field 'RSQR.Models.TipoReporte.Field')
  - [Internal](#F-RSQR-Models-TipoReporte-Internal 'RSQR.Models.TipoReporte.Internal')
  - [ZeroKm](#F-RSQR-Models-TipoReporte-ZeroKm 'RSQR.Models.TipoReporte.ZeroKm')
- [d4d5fields](#T-RSQR-Migrations-d4d5fields 'RSQR.Migrations.d4d5fields')
  - [BuildTargetModel()](#M-RSQR-Migrations-d4d5fields-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.d4d5fields.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-d4d5fields-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.d4d5fields.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-d4d5fields-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.d4d5fields.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [newContainment](#T-RSQR-Migrations-newContainment 'RSQR.Migrations.newContainment')
  - [BuildTargetModel()](#M-RSQR-Migrations-newContainment-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.newContainment.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-newContainment-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.newContainment.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-newContainment-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.newContainment.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [thirdStepData](#T-RSQR-Migrations-thirdStepData 'RSQR.Migrations.thirdStepData')
  - [BuildTargetModel()](#M-RSQR-Migrations-thirdStepData-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.thirdStepData.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-thirdStepData-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.thirdStepData.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-thirdStepData-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.thirdStepData.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')

<a name='T-RSQR-Controllers-AccountController'></a>
## AccountController `type`

##### Namespace

RSQR.Controllers

##### Summary

Controlador para gestionar la autenticación de usuarios.

<a name='M-RSQR-Controllers-AccountController-#ctor-RSQR-Services-ActiveDirectoryService-'></a>
### #ctor(adService) `constructor`

##### Summary

Constructor del controlador AccountController.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| adService | [RSQR.Services.ActiveDirectoryService](#T-RSQR-Services-ActiveDirectoryService 'RSQR.Services.ActiveDirectoryService') | Servicio de Active Directory para autenticación. |

<a name='M-RSQR-Controllers-AccountController-Login'></a>
### Login() `method`

##### Summary

Muestra la vista de inicio de sesión.

##### Returns

La vista de inicio de sesión.

##### Parameters

This method has no parameters.

<a name='M-RSQR-Controllers-AccountController-Login-System-String,System-String-'></a>
### Login(username,password) `method`

##### Summary

Procesa el formulario de inicio de sesión.

##### Returns

Redirige al usuario a la página de inicio si la autenticación es exitosa.
Muestra un mensaje de error en la vista de inicio de sesión si la autenticación falla.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| username | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Nombre de usuario. |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Contraseña del usuario. |

<a name='M-RSQR-Controllers-AccountController-Logout'></a>
### Logout() `method`

##### Summary

Cierra la sesión del usuario actual.

##### Returns

Redirige al usuario a la vista de inicio de sesión.

##### Parameters

This method has no parameters.

<a name='T-RSQR-Services-ActiveDirectoryService'></a>
## ActiveDirectoryService `type`

##### Namespace

RSQR.Services

##### Summary

Proporciona servicios para interactuar con Active Directory, incluyendo validación de credenciales
y recuperación de información de usuario.

<a name='M-RSQR-Services-ActiveDirectoryService-#ctor-System-String-'></a>
### #ctor(domain) `constructor`

##### Summary

Inicializa una nueva instancia del servicio de Active Directory.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| domain | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | El nombre de dominio del Active Directory al que conectarse. |

<a name='M-RSQR-Services-ActiveDirectoryService-GetDisplayName-System-String,System-String-'></a>
### GetDisplayName(username,password) `method`

##### Summary

Obtiene el nombre para mostrar (displayName) de un usuario desde Active Directory.

##### Returns

El nombre para mostrar del usuario si existe y las credenciales son válidas,
o string.Empty si no se encuentra, no tiene displayName o ocurre un error.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| username | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Nombre de usuario (sAMAccountName). |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Contraseña del usuario. |

##### Remarks

Requiere credenciales válidas para realizar la consulta.

<a name='M-RSQR-Services-ActiveDirectoryService-ValidateCredentials-System-String,System-String-'></a>
### ValidateCredentials(username,password) `method`

##### Summary

Valida las credenciales de un usuario contra el Active Directory.

##### Returns

True si las credenciales son válidas y el usuario existe en el directorio,
False en caso contrario o si ocurre un error.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| username | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Nombre de usuario (sAMAccountName) a validar. |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Contraseña del usuario. |

##### Remarks

Este método intenta autenticarse con el dominio usando las credenciales proporcionadas
y busca el usuario por su sAMAccountName.

<a name='T-AdUserManagerService'></a>
## AdUserManagerService `type`

##### Namespace



##### Summary

Servicio para sincronizar usuarios de Active Directory con el sistema de Identity.

<a name='M-AdUserManagerService-#ctor-Microsoft-AspNetCore-Identity-UserManager{Microsoft-AspNetCore-Identity-IdentityUser}-'></a>
### #ctor(userManager) `constructor`

##### Summary

Inicializa una nueva instancia del servicio AdUserManagerService.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| userManager | [Microsoft.AspNetCore.Identity.UserManager{Microsoft.AspNetCore.Identity.IdentityUser}](#T-Microsoft-AspNetCore-Identity-UserManager{Microsoft-AspNetCore-Identity-IdentityUser} 'Microsoft.AspNetCore.Identity.UserManager{Microsoft.AspNetCore.Identity.IdentityUser}') | El UserManager de ASP.NET Core Identity para gestionar usuarios. |

<a name='M-AdUserManagerService-SyncAdUserAsync-System-Security-Claims-ClaimsPrincipal-'></a>
### SyncAdUserAsync(principal) `method`

##### Summary

Sincroniza un usuario de Active Directory con el sistema de Identity.

##### Returns

Una tarea que representa la operación asíncrona.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| principal | [System.Security.Claims.ClaimsPrincipal](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Security.Claims.ClaimsPrincipal 'System.Security.Claims.ClaimsPrincipal') | ClaimsPrincipal que representa al usuario autenticado. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Se lanza cuando no se puede crear el usuario en el sistema de Identity. |

##### Remarks

Este método verifica si el usuario existe en Identity. Si no existe, crea un nuevo usuario
utilizando el nombre de usuario de AD y genera un correo electrónico predeterminado.

<a name='T-AdUserSyncMiddleware'></a>
## AdUserSyncMiddleware `type`

##### Namespace



##### Summary

Middleware para sincronizar automáticamente usuarios de Active Directory con el sistema de Identity
durante el procesamiento de cada solicitud HTTP autenticada.

<a name='M-AdUserSyncMiddleware-#ctor-Microsoft-AspNetCore-Http-RequestDelegate-'></a>
### #ctor(next) `constructor`

##### Summary

Inicializa una nueva instancia del middleware de sincronización de usuarios.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| next | [Microsoft.AspNetCore.Http.RequestDelegate](#T-Microsoft-AspNetCore-Http-RequestDelegate 'Microsoft.AspNetCore.Http.RequestDelegate') | El siguiente delegado en la pipeline de solicitudes HTTP. |

<a name='M-AdUserSyncMiddleware-InvokeAsync-Microsoft-AspNetCore-Http-HttpContext,AdUserManagerService-'></a>
### InvokeAsync(context,adUserManagerService) `method`

##### Summary

Método invocado por el runtime de ASP.NET Core para procesar las solicitudes HTTP.

##### Returns

Una tarea que representa la ejecución del middleware.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [Microsoft.AspNetCore.Http.HttpContext](#T-Microsoft-AspNetCore-Http-HttpContext 'Microsoft.AspNetCore.Http.HttpContext') | El contexto HTTP para la solicitud actual. |
| adUserManagerService | [AdUserManagerService](#T-AdUserManagerService 'AdUserManagerService') | Servicio de gestión de usuarios de AD inyectado por DI. |

##### Remarks

Este middleware verifica si el usuario está autenticado y, en ese caso,
invoca el servicio de sincronización de usuarios antes de pasar la solicitud
al siguiente middleware en la pipeline.

<a name='T-RSQR-Data-ApplicationDbContext'></a>
## ApplicationDbContext `type`

##### Namespace

RSQR.Data

##### Summary

Representa el contexto de la base de datos para la aplicación, incluyendo
las tablas de Identity y los modelos personalizados.

##### Remarks

Hereda de IdentityDbContext para integrar el sistema de autenticación ASP.NET Core Identity.

<a name='M-RSQR-Data-ApplicationDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RSQR-Data-ApplicationDbContext}-'></a>
### #ctor(options) `constructor`

##### Summary

Inicializa una nueva instancia del contexto de la base de datos.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.EntityFrameworkCore.DbContextOptions{RSQR.Data.ApplicationDbContext}](#T-Microsoft-EntityFrameworkCore-DbContextOptions{RSQR-Data-ApplicationDbContext} 'Microsoft.EntityFrameworkCore.DbContextOptions{RSQR.Data.ApplicationDbContext}') | Las opciones de configuración para este contexto. |

<a name='P-RSQR-Data-ApplicationDbContext-ConsecutivosArchivos'></a>
### ConsecutivosArchivos `property`

##### Summary

Obtiene o establece el DbSet para la entidad ConsecutivoArchivo.

<a name='P-RSQR-Data-ApplicationDbContext-PpmReports'></a>
### PpmReports `property`

##### Summary

Obtiene o establece el DbSet para la entidad PpmReport.

<a name='P-RSQR-Data-ApplicationDbContext-Reportes'></a>
### Reportes `property`

##### Summary

Obtiene o establece el DbSet para la entidad Reporte.

<a name='M-RSQR-Data-ApplicationDbContext-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### OnModelCreating(modelBuilder) `method`

##### Summary

Configura el modelo de la base de datos, incluyendo las relaciones y restricciones.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') | El constructor usado para crear el modelo. |

##### Remarks

Este método se llama cuando el modelo para un contexto derivado se inicializa.
Puede usarse para configurar el modelo descubierto por convención antes de que
se bloquee y se use para inicializar el contexto.

<a name='T-RSQR-Utilities-BusinessUnitMapping'></a>
## BusinessUnitMapping `type`

##### Namespace

RSQR.Utilities

<a name='M-RSQR-Utilities-BusinessUnitMapping-GetAllMappings'></a>
### GetAllMappings() `method`

##### Summary

Devuelve todos los mapeos disponibles (solo lectura).

##### Parameters

This method has no parameters.

<a name='M-RSQR-Utilities-BusinessUnitMapping-GetBusinessUnitCode-System-String-'></a>
### GetBusinessUnitCode(unitName) `method`

##### Summary

Obtiene el código abreviado de una unidad de negocio.

##### Returns

Código abreviado (ej. "ST"). Si no existe, devuelve las primeras 4 letras en mayúsculas.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| unitName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Nombre de la unidad de negocio (ej. "STARTER"). |

<a name='M-RSQR-Utilities-BusinessUnitMapping-IsValidBusinessUnit-System-String-'></a>
### IsValidBusinessUnit() `method`

##### Summary

Verifica si una unidad de negocio existe en el mapeo.

##### Parameters

This method has no parameters.

<a name='T-RSQR-Models-CincoMOpciones'></a>
## CincoMOpciones `type`

##### Namespace

RSQR.Models

##### Summary

Enumeración que representa las 5M's utilizadas en análisis de causa raíz.

<a name='F-RSQR-Models-CincoMOpciones-ManoDeObra'></a>
### ManoDeObra `constants`

##### Summary

Problema relacionado con personal/operadores

<a name='F-RSQR-Models-CincoMOpciones-Maquina'></a>
### Maquina `constants`

##### Summary

Problema relacionado con máquinas/equipos

<a name='F-RSQR-Models-CincoMOpciones-Material'></a>
### Material `constants`

##### Summary

Problema relacionado con materiales/materia prima

<a name='F-RSQR-Models-CincoMOpciones-Medicion'></a>
### Medicion `constants`

##### Summary

Problema relacionado con mediciones/calibraciones

<a name='F-RSQR-Models-CincoMOpciones-MedioAmbiente'></a>
### MedioAmbiente `constants`

##### Summary

Problema relacionado con condiciones ambientales

<a name='T-RSQR-Models-ConsecutivoArchivo'></a>
## ConsecutivoArchivo `type`

##### Namespace

RSQR.Models

##### Summary

Representa un consecutivo numérico para archivos, asociado a unidades de negocio.

##### Remarks

Esta entidad se utiliza para generar números consecutivos únicos por combinación
de unidad de negocio, código de negocio y año.

<a name='P-RSQR-Models-ConsecutivoArchivo-Anio'></a>
### Anio `property`

##### Summary

Año al que pertenece el consecutivo.

<a name='P-RSQR-Models-ConsecutivoArchivo-CodigoNegocio'></a>
### CodigoNegocio `property`

##### Summary

Código identificador del negocio.

##### Remarks

Requerido, con longitud máxima de 10 caracteres.

<a name='P-RSQR-Models-ConsecutivoArchivo-Consecutivo'></a>
### Consecutivo `property`

##### Summary

Número consecutivo actual.

##### Remarks

Valor por defecto: 1. Se incrementa automáticamente con cada nuevo uso.

<a name='P-RSQR-Models-ConsecutivoArchivo-FechaActualizacion'></a>
### FechaActualizacion `property`

##### Summary

Fecha y hora de la última actualización del registro.

##### Remarks

Se establece automáticamente a la fecha/hora actual al crearse o modificarse.

<a name='P-RSQR-Models-ConsecutivoArchivo-Id'></a>
### Id `property`

##### Summary

Identificador único del registro en la base de datos.

<a name='P-RSQR-Models-ConsecutivoArchivo-UnidadNegocio'></a>
### UnidadNegocio `property`

##### Summary

Nombre de la unidad de negocio asociada.

##### Remarks

Requerido, con longitud máxima de 50 caracteres.

<a name='T-RSQR-Services-EmailService'></a>
## EmailService `type`

##### Namespace

RSQR.Services

##### Summary

Servicio para el envío de correos electrónicos utilizando SMTP a través de MailKit.

<a name='M-RSQR-Services-EmailService-#ctor-Microsoft-Extensions-Options-IOptions{RSQR-Models-EmailSettings}-'></a>
### #ctor(emailSettings) `constructor`

##### Summary

Inicializa una nueva instancia del servicio de correo electrónico.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| emailSettings | [Microsoft.Extensions.Options.IOptions{RSQR.Models.EmailSettings}](#T-Microsoft-Extensions-Options-IOptions{RSQR-Models-EmailSettings} 'Microsoft.Extensions.Options.IOptions{RSQR.Models.EmailSettings}') | Configuración de correo electrónico inyectada mediante IOptions. |

<a name='M-RSQR-Services-EmailService-SendEmailAsync-System-String,System-String,System-String,System-String-'></a>
### SendEmailAsync(toEmail,subject,body) `method`

##### Summary

Envía un correo electrónico de forma asíncrona.

##### Returns

Una tarea que representa la operación asíncrona de envío de correo.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toEmail | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Dirección de correo electrónico del destinatario. |
| subject | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Asunto del correo electrónico. |
| body | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Cuerpo del mensaje en formato HTML. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Se puede lanzar cuando ocurre un error durante la conexión SMTP o el envío del correo. |

##### Remarks

Este método establece una conexión SMTP con el servidor configurado, envía el correo
y luego cierra la conexión. Utiliza SecureSocketOptions.Auto para negociar automáticamente
la seguridad de la conexión (SSL/TLS).

<a name='T-RSQR-Models-EmailSettings'></a>
## EmailSettings `type`

##### Namespace

RSQR.Models

##### Summary

Clase de configuración para los parámetros de envío de correos electrónicos.

##### Remarks

Esta clase se utiliza normalmente para ser inyectada mediante IOptions<EmailSettings>
y configurada desde appsettings.json.

<a name='P-RSQR-Models-EmailSettings-FromEmail'></a>
### FromEmail `property`

##### Summary

Dirección de correo electrónico que aparecerá como remitente.

##### Example

notificaciones@midominio.com

<a name='P-RSQR-Models-EmailSettings-SmtpPort'></a>
### SmtpPort `property`

##### Summary

Puerto del servidor SMTP.

##### Example

587

<a name='P-RSQR-Models-EmailSettings-SmtpServer'></a>
### SmtpServer `property`

##### Summary

Dirección del servidor SMTP para el envío de correos.

##### Example

smtp.midominio.com

<a name='T-RSQR-Models-ErrorViewModel'></a>
## ErrorViewModel `type`

##### Namespace

RSQR.Models

##### Summary

Modelo de vista para representar información de errores en la aplicación.

##### Remarks

Este modelo se utiliza típicamente en páginas de error para mostrar detalles
sobre la solicitud fallida.

<a name='P-RSQR-Models-ErrorViewModel-RequestId'></a>
### RequestId `property`

##### Summary

Obtiene o establece el ID único de la solicitud que generó el error.

##### Remarks

Puede ser nulo si no hay un ID de solicitud disponible.
Este valor se usa comúnmente para rastrear errores en los logs.

<a name='P-RSQR-Models-ErrorViewModel-ShowRequestId'></a>
### ShowRequestId `property`

##### Summary

Indica si se debe mostrar el ID de solicitud en la vista.

##### Remarks

Esta propiedad calculada se usa para controlar condicionalmente la visualización
del ID de solicitud en las vistas de error.

<a name='T-RSQR-Controllers-HomeController'></a>
## HomeController `type`

##### Namespace

RSQR.Controllers

##### Summary

Controlador principal de la aplicación. Gestiona las vistas de inicio, privacidad y errores.

##### Remarks

Este controlador está protegido con autorización, por lo que solo los usuarios autenticados pueden acceder a sus acciones.

<a name='M-RSQR-Controllers-HomeController-#ctor-Microsoft-Extensions-Logging-ILogger{RSQR-Controllers-HomeController}-'></a>
### #ctor(logger) `constructor`

##### Summary

Constructor del controlador HomeController.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| logger | [Microsoft.Extensions.Logging.ILogger{RSQR.Controllers.HomeController}](#T-Microsoft-Extensions-Logging-ILogger{RSQR-Controllers-HomeController} 'Microsoft.Extensions.Logging.ILogger{RSQR.Controllers.HomeController}') | Instancia de ILogger para registrar eventos y errores. |

<a name='M-RSQR-Controllers-HomeController-Error'></a>
### Error() `method`

##### Summary

Muestra la vista de error.

##### Returns

La vista de error con detalles del error ocurrido.

##### Parameters

This method has no parameters.

<a name='M-RSQR-Controllers-HomeController-Index'></a>
### Index() `method`

##### Summary

Muestra la vista de inicio (Index).

##### Returns

La vista de inicio.

##### Parameters

This method has no parameters.

<a name='T-RSQR-Services-IEmailService'></a>
## IEmailService `type`

##### Namespace

RSQR.Services

##### Summary

Interfaz que define el contrato para el servicio de envío de correos electrónicos.

<a name='M-RSQR-Services-IEmailService-SendEmailAsync-System-String,System-String,System-String,System-String-'></a>
### SendEmailAsync(toEmail,subject,body) `method`

##### Summary

Envía un correo electrónico de forma asíncrona.

##### Returns

Tarea que representa la operación asíncrona.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toEmail | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Dirección de correo electrónico del destinatario. |
| subject | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Asunto del mensaje. |
| body | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Contenido del mensaje en formato HTML. |

##### Remarks

Las implementaciones de esta interfaz deben proporcionar la lógica concreta
para enviar correos electrónicos utilizando algún proveedor de servicios SMTP.

<a name='T-PPMController'></a>
## PPMController `type`

##### Namespace



##### Summary

Controlador encargado de manejar las operaciones de cálculo de PPM (Parts Per Million).
Se conecta a bases de datos Oracle y SQL Server para obtener información de embarques y reclamaciones.
Requiere autenticación mediante el atributo [AuthorizeAttribute](#T-Microsoft-AspNetCore-Authorization-AuthorizeAttribute 'Microsoft.AspNetCore.Authorization.AuthorizeAttribute').

##### Remarks

Este controlador realiza cálculos de PPM en base a:
- Cantidad de piezas enviadas (Oracle).
- Cantidad de piezas defectuosas (SQL Server).

Además, soporta el manejo de grupos de descripciones predefinidas y casos especiales.

<a name='M-PPMController-#ctor-Microsoft-Extensions-Configuration-IConfiguration-'></a>
### #ctor(configuration) `constructor`

##### Summary

Constructor del controlador que inicializa las cadenas de conexión.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configuration | [Microsoft.Extensions.Configuration.IConfiguration](#T-Microsoft-Extensions-Configuration-IConfiguration 'Microsoft.Extensions.Configuration.IConfiguration') | Interfaz de configuración para acceder a appsettings.json |

<a name='F-PPMController-_configuration'></a>
### _configuration `constants`

##### Summary

Configuración de la aplicación para acceder a las cadenas de conexión.

<a name='F-PPMController-_gruposDescripciones'></a>
### _gruposDescripciones `constants`

##### Summary

Diccionario que mapea nombres de grupos a una lista de descripciones que los componen.
Utilizado para realizar consultas de forma agrupada.

##### Remarks

Ejemplos:
- "CM" incluye varios tipos de sistemas de control de motor.
- "EPS(3G)" agrupa piezas de dirección eléctrica de tercera generación.

<a name='F-PPMController-oracleConnectionString'></a>
### oracleConnectionString `constants`

##### Summary

Cadena de conexión para la base de datos Oracle.

<a name='F-PPMController-sqlServerConnectionString'></a>
### sqlServerConnectionString `constants`

##### Summary

Cadena de conexión para la base de datos SQL Server.

<a name='M-PPMController-GetPpmAnioFiscal'></a>
### GetPpmAnioFiscal() `method`

##### Summary

Obtiene el PPM acumulado para el año fiscal en curso.

##### Returns

Objeto JSON con el PPM y el rango de fechas del año fiscal.

##### Parameters

This method has no parameters.

##### Remarks

El año fiscal se considera desde el 1 de abril hasta el 31 de marzo.
Si la fecha actual es anterior al fin del año fiscal, se usa la fecha actual como límite.

<a name='M-PPMController-GetPpmMesActual'></a>
### GetPpmMesActual() `method`

##### Summary

Obtiene el PPM acumulado para el mes actual considerando todas las descripciones configuradas.

##### Returns

Objeto JSON con el valor PPM y la fecha de referencia.

##### Parameters

This method has no parameters.

<a name='M-PPMController-ObtenerDatosGrafico-System-String,System-String,System-String-'></a>
### ObtenerDatosGrafico(fechaInicio,fechaFin,descripcion) `method`

##### Summary

Obtiene datos mes a mes para generar un gráfico de PPM y reclamaciones en un rango de fechas.

##### Returns

Objeto JSON con:
- `labels`: lista de meses.
- `reclamos`: lista con el total de reclamaciones por mes.
- `ppm`: lista con los valores PPM por mes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fechaInicio | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Fecha inicial en formato `yyyy-MM-dd`. |
| fechaFin | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Fecha final en formato `yyyy-MM-dd`. |
| descripcion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Descripción del producto o grupo. |

<a name='M-PPMController-ObtenerSumaCuantosPSqlServer-System-String-'></a>
### ObtenerSumaCuantosPSqlServer(descripcion) `method`

##### Summary

Obtiene la cantidad de piezas defectuosas desde SQL Server para una descripción.

##### Returns

Total de piezas defectuosas.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| descripcion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Descripción del producto o grupo. |

<a name='M-PPMController-ObtenerTotalCajasOracle-System-String,System-String,System-String-'></a>
### ObtenerTotalCajasOracle(descripcion,fechaInicio,fechaFin) `method`

##### Summary

Obtiene el total de cajas enviadas desde Oracle para una descripción o grupo de descripciones en un rango de fechas.

##### Returns

Total de cajas enviadas.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| descripcion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Descripción o grupo. |
| fechaInicio | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Fecha inicial en formato `dd-MMM-yy`. |
| fechaFin | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Fecha final en formato `dd-MMM-yy`. |

<a name='M-PPMController-Ppm'></a>
### Ppm() `method`

##### Summary

Acción que devuelve la vista principal del módulo PPM.

##### Returns

Vista PPM

##### Parameters

This method has no parameters.

<a name='M-PPMController-SumarCajas-System-String,System-String,System-String-'></a>
### SumarCajas(fechaInicio,fechaFin,descripcion) `method`

##### Summary

Calcula el total de cajas enviadas y el valor PPM (Parts Per Million) para un rango de fechas y descripción especificados.

##### Returns

Objeto JSON con:
- `totalCajas`: total de cajas enviadas.
- `division`: valor PPM calculado.
- `error`: mensaje de error si aplica.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fechaInicio | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Fecha inicial en formato `yyyy-MM-dd`. |
| fechaFin | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Fecha final en formato `yyyy-MM-dd`. |
| descripcion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Descripción del producto o grupo de productos. |

##### Remarks

Fórmula de cálculo: `(Piezas defectuosas / Total de piezas enviadas) * 1,000,000`.

<a name='T-RSQR-Models-PpmReport'></a>
## PpmReport `type`

##### Namespace

RSQR.Models

##### Summary

Representa un reporte PPM (Parts Per Million) para el control de calidad y gestión de reclamos de clientes.

##### Remarks

Este modelo almacena información detallada sobre problemas de calidad reportados por clientes,
incluyendo investigación, responsabilidad y métricas de impacto.

<a name='P-RSQR-Models-PpmReport-Comentarios'></a>
### Comentarios `property`

##### Summary

Comentarios adicionales sobre el caso (opcional).

<a name='P-RSQR-Models-PpmReport-CuantosP'></a>
### CuantosP `property`

##### Summary

Cantidad de partes afectadas (opcional).

<a name='P-RSQR-Models-PpmReport-Customer'></a>
### Customer `property`

##### Summary

Nombre del cliente que reporta el problema.

##### Remarks

Campo obligatorio con longitud máxima de 100 caracteres.

<a name='P-RSQR-Models-PpmReport-CustomerClaimNumber'></a>
### CustomerClaimNumber `property`

##### Summary

Número de reclamo asignado por el cliente.

<a name='P-RSQR-Models-PpmReport-CustomerPartNumber'></a>
### CustomerPartNumber `property`

##### Summary

Número de parte según el cliente.

##### Remarks

Longitud máxima de 100 caracteres.

<a name='P-RSQR-Models-PpmReport-DateOfClose'></a>
### DateOfClose `property`

##### Summary

Fecha de cierre del reporte (opcional).

##### Remarks

Solo se completa cuando el caso está resuelto.

<a name='P-RSQR-Models-PpmReport-Fecha'></a>
### Fecha `property`

##### Summary

Fecha de creación del reporte.

<a name='P-RSQR-Models-PpmReport-Id'></a>
### Id `property`

##### Summary

Identificador único del reporte en la base de datos.

<a name='P-RSQR-Models-PpmReport-ImpactoPPM'></a>
### ImpactoPPM `property`

##### Summary

Indica si el problema impacta en las métricas PPM de la organización.

<a name='P-RSQR-Models-PpmReport-InvestigationReport'></a>
### InvestigationReport `property`

##### Summary

Reporte detallado de la investigación del problema.

<a name='P-RSQR-Models-PpmReport-Linea'></a>
### Linea `property`

##### Summary

Línea de producción o área relacionada con el problema.

<a name='P-RSQR-Models-PpmReport-Lote'></a>
### Lote `property`

##### Summary

Lote o batch del producto con problema (opcional).

<a name='P-RSQR-Models-PpmReport-Mileage'></a>
### Mileage `property`

##### Summary

Kilometraje o horas de uso (para productos en servicio).

##### Remarks

Debe ser un valor positivo.

<a name='P-RSQR-Models-PpmReport-MotherFactory'></a>
### MotherFactory `property`

##### Summary

Fábrica de origen del producto con problema.

##### Remarks

Longitud máxima de 100 caracteres.

<a name='P-RSQR-Models-PpmReport-NumParteAfectado'></a>
### NumParteAfectado `property`

##### Summary

Número de parte afectado (opcional).

<a name='P-RSQR-Models-PpmReport-Responsabilidad'></a>
### Responsabilidad `property`

##### Summary

Determina la responsabilidad asignada por el análisis de calidad.

<a name='P-RSQR-Models-PpmReport-Tipo'></a>
### Tipo `property`

##### Summary

Tipo de reporte según la clasificación interna.

<a name='P-RSQR-Models-PpmReport-TituloProblema'></a>
### TituloProblema `property`

##### Summary

Título descriptivo del problema (opcional).

<a name='T-RSQR-Models-Reporte'></a>
## Reporte `type`

##### Namespace

RSQR.Models

##### Summary

Modelo principal que representa un reporte de calidad en el sistema.

##### Remarks

Contiene toda la información relacionada con un problema de calidad reportado,
incluyendo análisis, acciones de contención y seguimiento.

<a name='P-RSQR-Models-Reporte-AP_NPR'></a>
### AP_NPR `property`

##### Summary

Acciones preventivas/No recurrencia

<a name='P-RSQR-Models-Reporte-AlertaCalidad'></a>
### AlertaCalidad `property`

##### Summary

Indica si se generó alerta de calidad

<a name='P-RSQR-Models-Reporte-CincoM'></a>
### CincoM `property`

##### Summary

Categoría según metodología 5M's

<a name='P-RSQR-Models-Reporte-Comentarios'></a>
### Comentarios `property`

##### Summary

Comentarios adicionales

<a name='P-RSQR-Models-Reporte-ComoP'></a>
### ComoP `property`

##### Summary

Cómo se detectó (How)

<a name='P-RSQR-Models-Reporte-ContencionActivity'></a>
### ContencionActivity `property`

##### Summary

Responsables de las acciones

<a name='P-RSQR-Models-Reporte-ContencionConsiderar'></a>
### ContencionConsiderar `property`

##### Summary

Actividades de contención

<a name='P-RSQR-Models-Reporte-ContencionEffectiveness'></a>
### ContencionEffectiveness `property`

##### Summary

Responsables de las acciones

<a name='P-RSQR-Models-Reporte-ContencionItems'></a>
### ContencionItems `property`

##### Summary

Ítems para acciones de contención

<a name='P-RSQR-Models-Reporte-ContencionNgParts'></a>
### ContencionNgParts `property`

##### Summary

Responsables de las acciones

<a name='P-RSQR-Models-Reporte-ContencionOkParts'></a>
### ContencionOkParts `property`

##### Summary

Responsables de las acciones

<a name='P-RSQR-Models-Reporte-ContencionResponsable'></a>
### ContencionResponsable `property`

##### Summary

Fechas de inicio de acciones

<a name='P-RSQR-Models-Reporte-ContencionTotalSuspeciousParts'></a>
### ContencionTotalSuspeciousParts `property`

##### Summary

Responsables de las acciones

<a name='P-RSQR-Models-Reporte-ControlesEstablecidos'></a>
### ControlesEstablecidos `property`

##### Summary

Controles existentes al momento del problema

<a name='P-RSQR-Models-Reporte-CuandoP'></a>
### CuandoP `property`

##### Summary

Cuándo ocurrió (When)

<a name='P-RSQR-Models-Reporte-CuantosP'></a>
### CuantosP `property`

##### Summary

Cuántos productos afectados (How many)

<a name='P-RSQR-Models-Reporte-Customer'></a>
### Customer `property`

##### Summary

Nombre del cliente afectado

<a name='P-RSQR-Models-Reporte-CustomerClaimDisplayNumber'></a>
### CustomerClaimDisplayNumber `property`

##### Summary

Número de reclamo formateado para visualización (no se persiste)

<a name='P-RSQR-Models-Reporte-CustomerClaimNumber'></a>
### CustomerClaimNumber `property`

##### Summary

Número de reclamo en base de datos

<a name='P-RSQR-Models-Reporte-CustomerClaimPrefix'></a>
### CustomerClaimPrefix `property`

##### Summary

Prefijo del cliente para formato de número (no se persiste)

<a name='P-RSQR-Models-Reporte-CustomerPartNumber'></a>
### CustomerPartNumber `property`

##### Summary

Número de parte según cliente

<a name='P-RSQR-Models-Reporte-DateOfClose'></a>
### DateOfClose `property`

##### Summary

Fecha de cierre del reporte

<a name='P-RSQR-Models-Reporte-Descripcion'></a>
### Descripcion `property`

##### Summary

Descripción general del problema

<a name='P-RSQR-Models-Reporte-DescripcionProblema'></a>
### DescripcionProblema `property`

##### Summary

Descripción detallada del problema

<a name='P-RSQR-Models-Reporte-Deteccion'></a>
### Deteccion `property`

##### Summary

Capacidad de detección

<a name='P-RSQR-Models-Reporte-Disposicion'></a>
### Disposicion `property`

##### Summary

Disposición del material afectado

<a name='P-RSQR-Models-Reporte-DondeP'></a>
### DondeP `property`

##### Summary

Dónde ocurrió (Where)

<a name='P-RSQR-Models-Reporte-EntrevistaInvolucrados'></a>
### EntrevistaInvolucrados `property`

##### Summary

Indica si se realizó entrevista a involucrados

<a name='P-RSQR-Models-Reporte-EvidenciaFotografica'></a>
### EvidenciaFotografica `property`

##### Summary

Evidencia fotográfica del problema

<a name='P-RSQR-Models-Reporte-Fecha'></a>
### Fecha `property`

##### Summary

Fecha de creación del reporte

<a name='P-RSQR-Models-Reporte-Id'></a>
### Id `property`

##### Summary

Identificador único del reporte

<a name='P-RSQR-Models-Reporte-ImpactoPPM'></a>
### ImpactoPPM `property`

##### Summary

Indica si impacta en métricas PPM

<a name='P-RSQR-Models-Reporte-InvestigationReport'></a>
### InvestigationReport `property`

##### Summary

Reporte final de investigación

<a name='P-RSQR-Models-Reporte-Linea'></a>
### Linea `property`

##### Summary

Línea o área donde se detectó el problema

<a name='P-RSQR-Models-Reporte-Lote'></a>
### Lote `property`

##### Summary

Lote o batch afectado

<a name='P-RSQR-Models-Reporte-Mileage'></a>
### Mileage `property`

##### Summary

Kilometraje/horas de uso (para field)

<a name='P-RSQR-Models-Reporte-ModoFalla'></a>
### ModoFalla `property`

##### Summary

Modo de falla identificado

<a name='P-RSQR-Models-Reporte-MotherFactory'></a>
### MotherFactory `property`

##### Summary

Fábrica de origen

<a name='P-RSQR-Models-Reporte-NumParteAfectado'></a>
### NumParteAfectado `property`

##### Summary

Número de parte afectada

<a name='P-RSQR-Models-Reporte-Ocurrencia'></a>
### Ocurrencia `property`

##### Summary

Frecuencia de ocurrencia

<a name='P-RSQR-Models-Reporte-PorqueP'></a>
### PorqueP `property`

##### Summary

Por qué ocurrió (Why)

<a name='P-RSQR-Models-Reporte-ProblemRank'></a>
### ProblemRank `property`

##### Summary

Nivel de prioridad/severidad del problema

<a name='P-RSQR-Models-Reporte-QueP'></a>
### QueP `property`

##### Summary

Qué ocurrió (What)

<a name='P-RSQR-Models-Reporte-QuienP'></a>
### QuienP `property`

##### Summary

Quién detectó (Who)

<a name='P-RSQR-Models-Reporte-Responsabilidad'></a>
### Responsabilidad `property`

##### Summary

Responsabilidad asignada

<a name='P-RSQR-Models-Reporte-Severidad'></a>
### Severidad `property`

##### Summary

Nivel de severidad del problema

<a name='P-RSQR-Models-Reporte-Tipo'></a>
### Tipo `property`

##### Summary

Tipo de reporte según clasificación

<a name='P-RSQR-Models-Reporte-TituloProblema'></a>
### TituloProblema `property`

##### Summary

Título descriptivo del problema

<a name='P-RSQR-Models-Reporte-UserName'></a>
### UserName `property`

##### Summary

Nombre del usuario que creó el reporte

<a name='T-RSQR-Models-ReporteViewModel'></a>
## ReporteViewModel `type`

##### Namespace

RSQR.Models

##### Summary

Modelo de vista para el formulario de creación/edición de reportes.

##### Remarks

Este ViewModel combina los datos del reporte con listas de selección
necesarias para los dropdowns en la vista.

<a name='M-RSQR-Models-ReporteViewModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Inicializa una nueva instancia del ViewModel con valores por defecto.

##### Parameters

This constructor has no parameters.

##### Remarks

Constructor que inicializa la lista ProblemRankList con las opciones:
-Seleccionar- (valor por defecto), Bajo, Medio y Alto.

<a name='P-RSQR-Models-ReporteViewModel-ProblemRankList'></a>
### ProblemRankList `property`

##### Summary

Obtiene o establece la lista de opciones para el campo ProblemRank.

##### Remarks

Lista predefinida de niveles de prioridad/severidad para selección en dropdown.
Incluye una opción por defecto "-Seleccionar-".

<a name='P-RSQR-Models-ReporteViewModel-Reporte'></a>
### Reporte `property`

##### Summary

Obtiene o establece el modelo de datos del reporte.

##### Remarks

Contiene todos los campos y propiedades del reporte de calidad.

<a name='T-RSQR-Controllers-ReportesController'></a>
## ReportesController `type`

##### Namespace

RSQR.Controllers

##### Summary

Controlador para gestionar los reportes del sistema.

##### Remarks

Este controlador está protegido con autorización, por lo que solo los usuarios autenticados pueden acceder a sus acciones.

<a name='M-RSQR-Controllers-ReportesController-#ctor-RSQR-Data-ApplicationDbContext,RSQR-Services-IEmailService,Microsoft-Extensions-Logging-ILogger{RSQR-Controllers-ReportesController}-'></a>
### #ctor(context) `constructor`

##### Summary

Constructor del controlador ReportesController.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [RSQR.Data.ApplicationDbContext](#T-RSQR-Data-ApplicationDbContext 'RSQR.Data.ApplicationDbContext') | Contexto de la base de datos. |

<a name='M-RSQR-Controllers-ReportesController-Create'></a>
### Create() `method`

##### Summary

Muestra el formulario para crear un nuevo reporte.

##### Returns

Una vista con el formulario de creación.

##### Parameters

This method has no parameters.

<a name='M-RSQR-Controllers-ReportesController-Create-RSQR-Models-Reporte,System-Collections-Generic-List{Microsoft-AspNetCore-Http-IFormFile}-'></a>
### Create(reporte,EvidenciaFotografica) `method`

##### Summary

Procesa el formulario de creación de un nuevo reporte.

##### Returns

Redirige a la lista de reportes si el modelo es válido; de lo contrario, muestra el formulario con errores.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reporte | [RSQR.Models.Reporte](#T-RSQR-Models-Reporte 'RSQR.Models.Reporte') | El objeto Reporte con los datos del formulario. |
| EvidenciaFotografica | [System.Collections.Generic.List{Microsoft.AspNetCore.Http.IFormFile}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{Microsoft.AspNetCore.Http.IFormFile}') | Lista de archivos de evidencia fotográfica. |

<a name='M-RSQR-Controllers-ReportesController-Delete-System-Nullable{System-Int32}-'></a>
### Delete(id) `method`

##### Summary

Muestra el formulario para confirmar la eliminación de un reporte.

##### Returns

Una vista con el formulario de confirmación.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | El ID del reporte a eliminar. |

<a name='M-RSQR-Controllers-ReportesController-DeleteConfirmed-System-Int32-'></a>
### DeleteConfirmed(id) `method`

##### Summary

Procesa la eliminación de un reporte.

##### Returns

Redirige a la lista de reportes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | El ID del reporte a eliminar. |

<a name='M-RSQR-Controllers-ReportesController-Details-System-Nullable{System-Int32}-'></a>
### Details(id) `method`

##### Summary

Muestra los detalles de un reporte específico.

##### Returns

Una vista con los detalles del reporte.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | El ID del reporte. |

<a name='M-RSQR-Controllers-ReportesController-Edit-System-Nullable{System-Int32}-'></a>
### Edit(id) `method`

##### Summary

Muestra el formulario para editar un reporte existente.

##### Returns

Una vista con el formulario de edición.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | El ID del reporte a editar. |

<a name='M-RSQR-Controllers-ReportesController-Edit-System-Int32,RSQR-Models-Reporte,System-Collections-Generic-List{Microsoft-AspNetCore-Http-IFormFile},System-Collections-Generic-List{System-Int32}-'></a>
### Edit(id,reporte) `method`

##### Summary

Procesa el formulario de edición de un reporte existente.

##### Returns

Redirige a la lista de reportes si el modelo es válido; de lo contrario, muestra el formulario con errores.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | El ID del reporte a editar. |
| reporte | [RSQR.Models.Reporte](#T-RSQR-Models-Reporte 'RSQR.Models.Reporte') | El objeto Reporte con los datos actualizados. |

<a name='M-RSQR-Controllers-ReportesController-ExportToExcel-System-Int32,System-String-'></a>
### ExportToExcel() `method`

##### Summary

Exporta los reportes a un archivo Excel.

##### Returns

Un archivo Excel con los datos de los reportes.

##### Parameters

This method has no parameters.

<a name='M-RSQR-Controllers-ReportesController-Index'></a>
### Index() `method`

##### Summary

Muestra la lista de todos los reportes.

##### Returns

Una vista con la lista de reportes.

##### Parameters

This method has no parameters.

<a name='M-RSQR-Controllers-ReportesController-ReporteExists-System-Int32-'></a>
### ReporteExists(id) `method`

##### Summary

Verifica si un reporte existe en la base de datos.

##### Returns

True si el reporte existe; de lo contrario, False.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | El ID del reporte. |

<a name='T-RSQR-Models-ResponsabilidadOpciones'></a>
## ResponsabilidadOpciones `type`

##### Namespace

RSQR.Models

##### Summary

Enumeración que define las posibles responsabilidades en un reporte.

<a name='F-RSQR-Models-ResponsabilidadOpciones-Meax'></a>
### Meax `constants`

##### Summary

Responsabilidad asignada a MEAX

<a name='F-RSQR-Models-ResponsabilidadOpciones-Proveedor'></a>
### Proveedor `constants`

##### Summary

Responsabilidad asignada a proveedor externo

<a name='T-RSQR-Models-TipoReporte'></a>
## TipoReporte `type`

##### Namespace

RSQR.Models

##### Summary

Enumeración que define los tipos de reportes disponibles en el sistema.

<a name='F-RSQR-Models-TipoReporte-Field'></a>
### Field `constants`

##### Summary

Reporte de problema en campo (vehículo en uso)

<a name='F-RSQR-Models-TipoReporte-Internal'></a>
### Internal `constants`

##### Summary

Reporte interno de calidad

<a name='F-RSQR-Models-TipoReporte-ZeroKm'></a>
### ZeroKm `constants`

##### Summary

Reporte de problema en vehículo nuevo (0km)

<a name='T-RSQR-Migrations-d4d5fields'></a>
## d4d5fields `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-d4d5fields-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-d4d5fields-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-d4d5fields-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RSQR-Migrations-newContainment'></a>
## newContainment `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-newContainment-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-newContainment-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-newContainment-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RSQR-Migrations-thirdStepData'></a>
## thirdStepData `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-thirdStepData-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-thirdStepData-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-thirdStepData-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

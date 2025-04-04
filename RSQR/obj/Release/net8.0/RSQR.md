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
- [AddConsecutivoArchivoTable](#T-RSQR-Migrations-AddConsecutivoArchivoTable 'RSQR.Migrations.AddConsecutivoArchivoTable')
  - [BuildTargetModel()](#M-RSQR-Migrations-AddConsecutivoArchivoTable-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.AddConsecutivoArchivoTable.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-AddConsecutivoArchivoTable-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.AddConsecutivoArchivoTable.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-AddConsecutivoArchivoTable-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.AddConsecutivoArchivoTable.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [BusinessUnitMapping](#T-RSQR-Utilities-BusinessUnitMapping 'RSQR.Utilities.BusinessUnitMapping')
  - [GetAllMappings()](#M-RSQR-Utilities-BusinessUnitMapping-GetAllMappings 'RSQR.Utilities.BusinessUnitMapping.GetAllMappings')
  - [GetBusinessUnitCode(unitName)](#M-RSQR-Utilities-BusinessUnitMapping-GetBusinessUnitCode-System-String- 'RSQR.Utilities.BusinessUnitMapping.GetBusinessUnitCode(System.String)')
  - [IsValidBusinessUnit()](#M-RSQR-Utilities-BusinessUnitMapping-IsValidBusinessUnit-System-String- 'RSQR.Utilities.BusinessUnitMapping.IsValidBusinessUnit(System.String)')
- [EmailService](#T-RSQR-Services-EmailService 'RSQR.Services.EmailService')
  - [#ctor(emailSettings)](#M-RSQR-Services-EmailService-#ctor-Microsoft-Extensions-Options-IOptions{RSQR-Models-EmailSettings}- 'RSQR.Services.EmailService.#ctor(Microsoft.Extensions.Options.IOptions{RSQR.Models.EmailSettings})')
  - [SendEmailAsync(toEmail,subject,body)](#M-RSQR-Services-EmailService-SendEmailAsync-System-String,System-String,System-String- 'RSQR.Services.EmailService.SendEmailAsync(System.String,System.String,System.String)')
- [HomeController](#T-RSQR-Controllers-HomeController 'RSQR.Controllers.HomeController')
  - [#ctor(logger)](#M-RSQR-Controllers-HomeController-#ctor-Microsoft-Extensions-Logging-ILogger{RSQR-Controllers-HomeController}- 'RSQR.Controllers.HomeController.#ctor(Microsoft.Extensions.Logging.ILogger{RSQR.Controllers.HomeController})')
  - [Error()](#M-RSQR-Controllers-HomeController-Error 'RSQR.Controllers.HomeController.Error')
  - [Index()](#M-RSQR-Controllers-HomeController-Index 'RSQR.Controllers.HomeController.Index')
- [IEmailService](#T-RSQR-Services-IEmailService 'RSQR.Services.IEmailService')
  - [SendEmailAsync(toEmail,subject,body)](#M-RSQR-Services-IEmailService-SendEmailAsync-System-String,System-String,System-String- 'RSQR.Services.IEmailService.SendEmailAsync(System.String,System.String,System.String)')
- [NewMigration](#T-RSQR-Migrations-NewMigration 'RSQR.Migrations.NewMigration')
  - [BuildTargetModel()](#M-RSQR-Migrations-NewMigration-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.NewMigration.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-NewMigration-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.NewMigration.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-NewMigration-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.NewMigration.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [ReportesController](#T-RSQR-Controllers-ReportesController 'RSQR.Controllers.ReportesController')
  - [#ctor(context)](#M-RSQR-Controllers-ReportesController-#ctor-RSQR-Data-ApplicationDbContext,RSQR-Services-IEmailService- 'RSQR.Controllers.ReportesController.#ctor(RSQR.Data.ApplicationDbContext,RSQR.Services.IEmailService)')
  - [Create()](#M-RSQR-Controllers-ReportesController-Create 'RSQR.Controllers.ReportesController.Create')
  - [Create(reporte,EvidenciaFotografica)](#M-RSQR-Controllers-ReportesController-Create-RSQR-Models-Reporte,System-Collections-Generic-List{Microsoft-AspNetCore-Http-IFormFile}- 'RSQR.Controllers.ReportesController.Create(RSQR.Models.Reporte,System.Collections.Generic.List{Microsoft.AspNetCore.Http.IFormFile})')
  - [Delete(id)](#M-RSQR-Controllers-ReportesController-Delete-System-Nullable{System-Int32}- 'RSQR.Controllers.ReportesController.Delete(System.Nullable{System.Int32})')
  - [DeleteConfirmed(id)](#M-RSQR-Controllers-ReportesController-DeleteConfirmed-System-Int32- 'RSQR.Controllers.ReportesController.DeleteConfirmed(System.Int32)')
  - [Details(id)](#M-RSQR-Controllers-ReportesController-Details-System-Nullable{System-Int32}- 'RSQR.Controllers.ReportesController.Details(System.Nullable{System.Int32})')
  - [Edit(id)](#M-RSQR-Controllers-ReportesController-Edit-System-Nullable{System-Int32}- 'RSQR.Controllers.ReportesController.Edit(System.Nullable{System.Int32})')
  - [Edit(id,reporte)](#M-RSQR-Controllers-ReportesController-Edit-System-Int32,RSQR-Models-Reporte- 'RSQR.Controllers.ReportesController.Edit(System.Int32,RSQR.Models.Reporte)')
  - [ExportToExcel()](#M-RSQR-Controllers-ReportesController-ExportToExcel-System-String- 'RSQR.Controllers.ReportesController.ExportToExcel(System.String)')
  - [Index()](#M-RSQR-Controllers-ReportesController-Index 'RSQR.Controllers.ReportesController.Index')
  - [ReporteExists(id)](#M-RSQR-Controllers-ReportesController-ReporteExists-System-Int32- 'RSQR.Controllers.ReportesController.ReporteExists(System.Int32)')
- [UpdateCincoMField](#T-RSQR-Migrations-UpdateCincoMField 'RSQR.Migrations.UpdateCincoMField')
  - [BuildTargetModel()](#M-RSQR-Migrations-UpdateCincoMField-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.UpdateCincoMField.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-UpdateCincoMField-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.UpdateCincoMField.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-UpdateCincoMField-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.UpdateCincoMField.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [UpdateImpactoPPMType](#T-RSQR-Migrations-UpdateImpactoPPMType 'RSQR.Migrations.UpdateImpactoPPMType')
  - [BuildTargetModel()](#M-RSQR-Migrations-UpdateImpactoPPMType-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.UpdateImpactoPPMType.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-UpdateImpactoPPMType-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.UpdateImpactoPPMType.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-UpdateImpactoPPMType-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.UpdateImpactoPPMType.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [UpdateResponsabilidadField](#T-RSQR-Migrations-UpdateResponsabilidadField 'RSQR.Migrations.UpdateResponsabilidadField')
  - [BuildTargetModel()](#M-RSQR-Migrations-UpdateResponsabilidadField-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.UpdateResponsabilidadField.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-UpdateResponsabilidadField-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.UpdateResponsabilidadField.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-UpdateResponsabilidadField-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.UpdateResponsabilidadField.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [UpdateTipoField](#T-RSQR-Migrations-UpdateTipoField 'RSQR.Migrations.UpdateTipoField')
  - [BuildTargetModel()](#M-RSQR-Migrations-UpdateTipoField-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RSQR.Migrations.UpdateTipoField.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down()](#M-RSQR-Migrations-UpdateTipoField-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.UpdateTipoField.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up()](#M-RSQR-Migrations-UpdateTipoField-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RSQR.Migrations.UpdateTipoField.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')

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

<a name='T-RSQR-Migrations-AddConsecutivoArchivoTable'></a>
## AddConsecutivoArchivoTable `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-AddConsecutivoArchivoTable-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-AddConsecutivoArchivoTable-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-AddConsecutivoArchivoTable-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

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

<a name='M-RSQR-Services-EmailService-SendEmailAsync-System-String,System-String,System-String-'></a>
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

<a name='M-RSQR-Services-IEmailService-SendEmailAsync-System-String,System-String,System-String-'></a>
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

<a name='T-RSQR-Migrations-NewMigration'></a>
## NewMigration `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-NewMigration-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-NewMigration-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-NewMigration-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RSQR-Controllers-ReportesController'></a>
## ReportesController `type`

##### Namespace

RSQR.Controllers

##### Summary

Controlador para gestionar los reportes del sistema.

##### Remarks

Este controlador está protegido con autorización, por lo que solo los usuarios autenticados pueden acceder a sus acciones.

<a name='M-RSQR-Controllers-ReportesController-#ctor-RSQR-Data-ApplicationDbContext,RSQR-Services-IEmailService-'></a>
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

<a name='M-RSQR-Controllers-ReportesController-Edit-System-Int32,RSQR-Models-Reporte-'></a>
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

<a name='M-RSQR-Controllers-ReportesController-ExportToExcel-System-String-'></a>
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

<a name='T-RSQR-Migrations-UpdateCincoMField'></a>
## UpdateCincoMField `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-UpdateCincoMField-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-UpdateCincoMField-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-UpdateCincoMField-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RSQR-Migrations-UpdateImpactoPPMType'></a>
## UpdateImpactoPPMType `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-UpdateImpactoPPMType-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-UpdateImpactoPPMType-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-UpdateImpactoPPMType-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RSQR-Migrations-UpdateResponsabilidadField'></a>
## UpdateResponsabilidadField `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-UpdateResponsabilidadField-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-UpdateResponsabilidadField-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-UpdateResponsabilidadField-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RSQR-Migrations-UpdateTipoField'></a>
## UpdateTipoField `type`

##### Namespace

RSQR.Migrations

##### Summary

*Inherit from parent.*

<a name='M-RSQR-Migrations-UpdateTipoField-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-UpdateTipoField-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RSQR-Migrations-UpdateTipoField-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

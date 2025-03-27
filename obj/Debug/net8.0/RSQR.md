<a name='assembly'></a>
# RSQR

## Contents

- [AccountController](#T-RSQR-Controllers-AccountController 'RSQR.Controllers.AccountController')
  - [#ctor(adService)](#M-RSQR-Controllers-AccountController-#ctor-RSQR-Services-ActiveDirectoryService- 'RSQR.Controllers.AccountController.#ctor(RSQR.Services.ActiveDirectoryService)')
  - [Login()](#M-RSQR-Controllers-AccountController-Login 'RSQR.Controllers.AccountController.Login')
  - [Login(username,password)](#M-RSQR-Controllers-AccountController-Login-System-String,System-String- 'RSQR.Controllers.AccountController.Login(System.String,System.String)')
  - [Logout()](#M-RSQR-Controllers-AccountController-Logout 'RSQR.Controllers.AccountController.Logout')
- [HomeController](#T-RSQR-Controllers-HomeController 'RSQR.Controllers.HomeController')
  - [#ctor(logger)](#M-RSQR-Controllers-HomeController-#ctor-Microsoft-Extensions-Logging-ILogger{RSQR-Controllers-HomeController}- 'RSQR.Controllers.HomeController.#ctor(Microsoft.Extensions.Logging.ILogger{RSQR.Controllers.HomeController})')
  - [Error()](#M-RSQR-Controllers-HomeController-Error 'RSQR.Controllers.HomeController.Error')
  - [Index()](#M-RSQR-Controllers-HomeController-Index 'RSQR.Controllers.HomeController.Index')
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
  - [ExportToExcel()](#M-RSQR-Controllers-ReportesController-ExportToExcel 'RSQR.Controllers.ReportesController.ExportToExcel')
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

<a name='M-RSQR-Controllers-ReportesController-ExportToExcel'></a>
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

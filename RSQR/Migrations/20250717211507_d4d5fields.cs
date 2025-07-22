using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSQR.Migrations
{
    /// <inheritdoc />
    public partial class d4d5fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefectoDuplicado",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionAction",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionAmef",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCEnvironment",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCEnvironmentC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCMachinary",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCMachinaryC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCManpower",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCManpowerC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCMaterial",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCMaterialC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCMeasurement",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCMeasurementC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCMethod",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCMethodC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCProcessName",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCProcessNameC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCRank",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCRankC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCloseDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionCp",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionDepartment",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionItems",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionOpeningDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetectionResponsable",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorDos",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorDosCorrectiveActions",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorDosCuartoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorDosPrimerWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorDosQuintoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorDosSegundoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorDosTercerWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorTres",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorTresCorrectiveActions",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorTresCuartoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorTresPrimerWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorTresQuintoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorTresSegundoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorTresTercerWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorUno",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorUnoCorrectiveActions",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorUnoCuartoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorUnoPrimerWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorUnoQuintoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorUnoSegundoWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorUnoTercerWhy",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fm6Ms",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FmContentionActions",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FmFactorDos",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FmFactorTres",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FmFactorUno",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FmMode",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FmProcessName",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FmRelated",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccurrenceAction",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccurrenceAmef",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccurrenceCloseDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccurrenceCp",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccurrenceDepartment",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccurrenceItems",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccurrenceOpeningDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccurrenceResponsable",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCEnvironment",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCEnvironmentC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCMachinary",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCMachinaryC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCManpower",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCManpowerC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCMaterial",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCMaterialC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCMeasurement",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCMeasurementC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCMethod",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCMethodC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCProcessName",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCProcessNameC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCRank",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreventiveCRankC",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefectoDuplicado",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionAction",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionAmef",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCEnvironment",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCEnvironmentC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCMachinary",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCMachinaryC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCManpower",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCManpowerC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCMaterial",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCMaterialC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCMeasurement",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCMeasurementC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCMethod",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCMethodC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCProcessName",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCProcessNameC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCRank",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCRankC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCloseDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionCp",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionDepartment",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionItems",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionOpeningDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "DetectionResponsable",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorDos",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorDosCorrectiveActions",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorDosCuartoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorDosPrimerWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorDosQuintoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorDosSegundoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorDosTercerWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorTres",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorTresCorrectiveActions",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorTresCuartoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorTresPrimerWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorTresQuintoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorTresSegundoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorTresTercerWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorUno",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorUnoCorrectiveActions",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorUnoCuartoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorUnoPrimerWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorUnoQuintoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorUnoSegundoWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FactorUnoTercerWhy",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "Fm6Ms",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FmContentionActions",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FmFactorDos",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FmFactorTres",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FmFactorUno",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FmMode",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FmProcessName",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "FmRelated",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "OccurrenceAction",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "OccurrenceAmef",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "OccurrenceCloseDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "OccurrenceCp",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "OccurrenceDepartment",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "OccurrenceItems",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "OccurrenceOpeningDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "OccurrenceResponsable",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCEnvironment",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCEnvironmentC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCMachinary",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCMachinaryC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCManpower",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCManpowerC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCMaterial",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCMaterialC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCMeasurement",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCMeasurementC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCMethod",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCMethodC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCProcessName",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCProcessNameC",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCRank",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "PreventiveCRankC",
                table: "qcReport");
        }
    }
}

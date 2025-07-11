using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSQR.Migrations
{
    /// <inheritdoc />
    public partial class RenameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PpmReports_Reportes_Id",
                table: "PpmReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reportes",
                table: "Reportes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PpmReports",
                table: "PpmReports");

            migrationBuilder.RenameTable(
                name: "Reportes",
                newName: "qcReport");

            migrationBuilder.RenameTable(
                name: "PpmReports",
                newName: "qcPpmReport");

            migrationBuilder.AddPrimaryKey(
                name: "PK_qcReport",
                table: "qcReport",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_qcPpmReport",
                table: "qcPpmReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_qcPpmReport_qcReport_Id",
                table: "qcPpmReport",
                column: "Id",
                principalTable: "qcReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_qcPpmReport_qcReport_Id",
                table: "qcPpmReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_qcReport",
                table: "qcReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_qcPpmReport",
                table: "qcPpmReport");

            migrationBuilder.RenameTable(
                name: "qcReport",
                newName: "Reportes");

            migrationBuilder.RenameTable(
                name: "qcPpmReport",
                newName: "PpmReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reportes",
                table: "Reportes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PpmReports",
                table: "PpmReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PpmReports_Reportes_Id",
                table: "PpmReports",
                column: "Id",
                principalTable: "Reportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSQR.Migrations
{
    /// <inheritdoc />
    public partial class newContainment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

           

            migrationBuilder.CreateTable(
                name: "qcReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProblemRank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    TituloProblema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CincoM = table.Column<int>(type: "int", nullable: false),
                    NumParteAfectado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionProblema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PorqueP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DondeP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuienP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuandoP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuantosP = table.Column<int>(type: "int", nullable: true),
                    ComoP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerClaimNumber = table.Column<int>(type: "int", nullable: false),
                    ContencionItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContencionConsiderar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContencionActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContencionResponsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContencionTotalSuspeciousParts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContencionOkParts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContencionNgParts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContencionEffectiveness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlertaCalidad = table.Column<bool>(type: "bit", nullable: false),
                    Disposicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntrevistaInvolucrados = table.Column<bool>(type: "bit", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Severidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ocurrencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deteccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AP_NPR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModoFalla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlesEstablecidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvidenciaFotografica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherFactory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mileage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvestigationReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfClose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImpactoPPM = table.Column<bool>(type: "bit", nullable: false),
                    Responsabilidad = table.Column<int>(type: "int", nullable: false),
                    NombreCar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerReport = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qcReport", x => x.Id);
                });

            

            

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ConsecutivosArchivos");

            migrationBuilder.DropTable(
                name: "qcPpmReport");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "qcReport");
        }
    }
}

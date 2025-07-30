using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSQR.Migrations
{
    /// <inheritdoc />
    public partial class thirdStepData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Approval",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D7D8ActualClosingDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D7D8CodeDescription",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D7D8Comments",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D7D8Deadline",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D7D8Documentation",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D7D8Responsible",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D7D8Sn",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerDetectionAction",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerDetectionAmef",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerDetectionCloseDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerDetectionCp",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerDetectionDepartment",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerDetectionItems",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerDetectionOpeningDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerDetectionResponsable",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerOccurrenceAction",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerOccurrenceAmef",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerOccurrenceCloseDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerOccurrenceCp",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerOccurrenceDepartment",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerOccurrenceItems",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerOccurrenceOpeningDate",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerOccurrenceResponsable",
                table: "qcReport",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approval",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "D7D8ActualClosingDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "D7D8CodeDescription",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "D7D8Comments",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "D7D8Deadline",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "D7D8Documentation",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "D7D8Responsible",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "D7D8Sn",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerDetectionAction",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerDetectionAmef",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerDetectionCloseDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerDetectionCp",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerDetectionDepartment",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerDetectionItems",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerDetectionOpeningDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerDetectionResponsable",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerOccurrenceAction",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerOccurrenceAmef",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerOccurrenceCloseDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerOccurrenceCp",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerOccurrenceDepartment",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerOccurrenceItems",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerOccurrenceOpeningDate",
                table: "qcReport");

            migrationBuilder.DropColumn(
                name: "VerOccurrenceResponsable",
                table: "qcReport");
        }
    }
}

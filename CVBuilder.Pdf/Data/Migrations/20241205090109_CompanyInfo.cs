using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.Pdf.Data.Migrations
{
    /// <inheritdoc />
    public partial class CompanyInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PDFTemplates",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PDFTemplates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PDFTemplates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PDFTemplates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "PDFTemplates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "PDFTemplates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "PDFTemplates");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PDFTemplates");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PDFTemplates");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PDFTemplates");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "PDFTemplates");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "PDFTemplates");
        }
    }
}

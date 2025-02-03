using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.Profiles.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDescriptionSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Profiles",
                type: "nvarchar(540)",
                maxLength: 540,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Profiles",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(540)",
                oldMaxLength: 540);
        }
    }
}

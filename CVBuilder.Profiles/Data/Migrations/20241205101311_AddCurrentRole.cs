using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVBuilder.Profiles.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentRole",
                table: "Profiles",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentRole",
                table: "Profiles");
        }
    }
}

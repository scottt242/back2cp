using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimaryConnect.Migrations
{
    /// <inheritdoc />
    public partial class Migration222A1wl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsJustified",
                table: "absences");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "absences",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "absences");

            migrationBuilder.AddColumn<bool>(
                name: "IsJustified",
                table: "absences",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}

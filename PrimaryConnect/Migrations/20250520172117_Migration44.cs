using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimaryConnect.Migrations
{
    /// <inheritdoc />
    public partial class Migration44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "homeworks");

            migrationBuilder.AddColumn<bool>(
                name: "AssignedToall",
                table: "resources",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "resources",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "date",
                table: "resources",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "level",
                table: "resources",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ClassId",
                table: "homeworks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "teacherId",
                table: "homeworks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_resources_TeacherId",
                table: "resources",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_resources_Teacher_TeacherId",
                table: "resources",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_resources_Teacher_TeacherId",
                table: "resources");

            migrationBuilder.DropIndex(
                name: "IX_resources_TeacherId",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "AssignedToall",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "date",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "level",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "teacherId",
                table: "homeworks");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "homeworks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "homeworks",
                type: "INTEGER",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimaryConnect.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_notifications",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "TargetClasses",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "TargetRoles",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "UserIds",
                table: "notifications");

            migrationBuilder.RenameTable(
                name: "notifications",
                newName: "NotificationRequest");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "NotificationRequest",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "NotificationRequest",
                newName: "UserType");

            migrationBuilder.RenameColumn(
                name: "SendToAll",
                table: "NotificationRequest",
                newName: "UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "NotificationRequest",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationRequest",
                table: "NotificationRequest",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationRequest",
                table: "NotificationRequest");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "NotificationRequest");

            migrationBuilder.RenameTable(
                name: "NotificationRequest",
                newName: "notifications");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "notifications",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "notifications",
                newName: "SendToAll");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "notifications",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "SentAt",
                table: "notifications",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TargetClasses",
                table: "notifications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetRoles",
                table: "notifications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIds",
                table: "notifications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_notifications",
                table: "notifications",
                column: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlkAdmin.Migrations
{
    /// <inheritdoc />
    public partial class user_entity_update_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthdayDate",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IrlName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LobbyName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TgLink",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthdayDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IrlName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LobbyName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TgLink",
                table: "Users");
        }
    }
}

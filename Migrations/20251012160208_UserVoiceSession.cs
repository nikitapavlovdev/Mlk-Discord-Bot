using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlkAdmin.Migrations
{
    /// <inheritdoc />
    public partial class UserVoiceSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Messages",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "VoiceSession",
                columns: table => new
                {
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VoiceStarting = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalSeconds = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceSession", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoiceSession");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Messages",
                newName: "Id");
        }
    }
}

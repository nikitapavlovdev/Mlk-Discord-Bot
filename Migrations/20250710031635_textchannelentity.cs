using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlkAdmin.Migrations
{
    /// <inheritdoc />
    public partial class textchannelentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Https",
                table: "Voices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Https",
                table: "Voices",
                type: "TEXT",
                nullable: true);
        }
    }
}

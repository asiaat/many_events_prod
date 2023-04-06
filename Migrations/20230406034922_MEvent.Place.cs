using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyEvents.Migrations
{
    /// <inheritdoc />
    public partial class MEventPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "MEvent",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place",
                table: "MEvent");
        }
    }
}

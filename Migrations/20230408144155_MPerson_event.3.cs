using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyEvents.Migrations
{
    /// <inheritdoc />
    public partial class MPerson_event3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MPersonId",
                table: "MEvent",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MEvent_MPersonId",
                table: "MEvent",
                column: "MPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_MEvent_MPerson_MPersonId",
                table: "MEvent",
                column: "MPersonId",
                principalTable: "MPerson",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MEvent_MPerson_MPersonId",
                table: "MEvent");

            migrationBuilder.DropIndex(
                name: "IX_MEvent_MPersonId",
                table: "MEvent");

            migrationBuilder.DropColumn(
                name: "MPersonId",
                table: "MEvent");
        }
    }
}

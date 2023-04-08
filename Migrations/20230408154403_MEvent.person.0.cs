using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyEvents.Migrations
{
    /// <inheritdoc />
    public partial class MEventperson0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "MEventMPerson",
                columns: table => new
                {
                    EventsListId = table.Column<int>(type: "INTEGER", nullable: false),
                    MPersonsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEventMPerson", x => new { x.EventsListId, x.MPersonsId });
                    table.ForeignKey(
                        name: "FK_MEventMPerson_MEvent_EventsListId",
                        column: x => x.EventsListId,
                        principalTable: "MEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MEventMPerson_MPerson_MPersonsId",
                        column: x => x.MPersonsId,
                        principalTable: "MPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MEventMPerson_MPersonsId",
                table: "MEventMPerson",
                column: "MPersonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MEventMPerson");

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
    }
}

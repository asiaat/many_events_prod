using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyEvents.Migrations
{
    /// <inheritdoc />
    public partial class MCompany2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MEvent_MCompany_MCompanyId",
                table: "MEvent");

            migrationBuilder.DropIndex(
                name: "IX_MEvent_MCompanyId",
                table: "MEvent");

            migrationBuilder.DropColumn(
                name: "MCompanyId",
                table: "MEvent");

            migrationBuilder.CreateTable(
                name: "MCompanyMEvent",
                columns: table => new
                {
                    EventsListId = table.Column<int>(type: "INTEGER", nullable: false),
                    MCompaniesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCompanyMEvent", x => new { x.EventsListId, x.MCompaniesId });
                    table.ForeignKey(
                        name: "FK_MCompanyMEvent_MCompany_MCompaniesId",
                        column: x => x.MCompaniesId,
                        principalTable: "MCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MCompanyMEvent_MEvent_EventsListId",
                        column: x => x.EventsListId,
                        principalTable: "MEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MCompanyMEvent_MCompaniesId",
                table: "MCompanyMEvent",
                column: "MCompaniesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MCompanyMEvent");

            migrationBuilder.AddColumn<int>(
                name: "MCompanyId",
                table: "MEvent",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MEvent_MCompanyId",
                table: "MEvent",
                column: "MCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MEvent_MCompany_MCompanyId",
                table: "MEvent",
                column: "MCompanyId",
                principalTable: "MCompany",
                principalColumn: "Id");
        }
    }
}

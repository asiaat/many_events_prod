using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyEvents.Migrations
{
    /// <inheritdoc />
    public partial class MCompany1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MCompanyId",
                table: "MEvent",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JurName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    RegCode = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    GuestsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    FeeTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MCompany_MFeeType_FeeTypeId",
                        column: x => x.FeeTypeId,
                        principalTable: "MFeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MEvent_MCompanyId",
                table: "MEvent",
                column: "MCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MCompany_FeeTypeId",
                table: "MCompany",
                column: "FeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MEvent_MCompany_MCompanyId",
                table: "MEvent",
                column: "MCompanyId",
                principalTable: "MCompany",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MEvent_MCompany_MCompanyId",
                table: "MEvent");

            migrationBuilder.DropTable(
                name: "MCompany");

            migrationBuilder.DropIndex(
                name: "IX_MEvent_MCompanyId",
                table: "MEvent");

            migrationBuilder.DropColumn(
                name: "MCompanyId",
                table: "MEvent");
        }
    }
}

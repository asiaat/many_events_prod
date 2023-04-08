using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyEvents.Migrations
{
    /// <inheritdoc />
    public partial class Person0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalCode",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalCode", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "MPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PersonalCodeCode = table.Column<string>(type: "TEXT", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: false),
                    FeeTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MPerson_MFeeType_FeeTypeId",
                        column: x => x.FeeTypeId,
                        principalTable: "MFeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MPerson_PersonalCode_PersonalCodeCode",
                        column: x => x.PersonalCodeCode,
                        principalTable: "PersonalCode",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MPerson_FeeTypeId",
                table: "MPerson",
                column: "FeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MPerson_PersonalCodeCode",
                table: "MPerson",
                column: "PersonalCodeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MPerson");

            migrationBuilder.DropTable(
                name: "PersonalCode");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManyEvents.Migrations
{
    /// <inheritdoc />
    public partial class MEventfeetype0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventFeeTypeId",
                table: "MEvent",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MEvent_EventFeeTypeId",
                table: "MEvent",
                column: "EventFeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MEvent_MFeeType_EventFeeTypeId",
                table: "MEvent",
                column: "EventFeeTypeId",
                principalTable: "MFeeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MEvent_MFeeType_EventFeeTypeId",
                table: "MEvent");

            migrationBuilder.DropIndex(
                name: "IX_MEvent_EventFeeTypeId",
                table: "MEvent");

            migrationBuilder.DropColumn(
                name: "EventFeeTypeId",
                table: "MEvent");
        }
    }
}

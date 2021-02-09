using Microsoft.EntityFrameworkCore.Migrations;

namespace CostJanitor.Infrastructure.Migrations
{
    public partial class xax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItemReportItem_ReportItem_CostItemsId",
                table: "CostItemReportItem");

            migrationBuilder.RenameColumn(
                name: "CostItemsId",
                table: "CostItemReportItem",
                newName: "_costItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItemReportItem_ReportItem__costItemsId",
                table: "CostItemReportItem",
                column: "_costItemsId",
                principalTable: "ReportItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItemReportItem_ReportItem__costItemsId",
                table: "CostItemReportItem");

            migrationBuilder.RenameColumn(
                name: "_costItemsId",
                table: "CostItemReportItem",
                newName: "CostItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItemReportItem_ReportItem_CostItemsId",
                table: "CostItemReportItem",
                column: "CostItemsId",
                principalTable: "ReportItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

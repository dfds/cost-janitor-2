using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostJanitor.Infrastructure.Migrations
{
    public partial class xaxa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItemReportItem_CostItem_CostItemsLabel_CostItemsCapabil~",
                table: "CostItemReportItem");

            migrationBuilder.RenameColumn(
                name: "CostItemsCapabilityIdentifier",
                table: "CostItemReportItem",
                newName: "_costItemsCapabilityIdentifier");

            migrationBuilder.RenameColumn(
                name: "CostItemsLabel",
                table: "CostItemReportItem",
                newName: "_costItemsLabel");

            migrationBuilder.RenameIndex(
                name: "IX_CostItemReportItem_CostItemsLabel_CostItemsCapabilityIdenti~",
                table: "CostItemReportItem",
                newName: "IX_CostItemReportItem__costItemsLabel__costItemsCapabilityIden~");

            migrationBuilder.AddColumn<Guid>(
                name: "ReportItemId",
                table: "CostItem",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostItem_ReportItemId",
                table: "CostItem",
                column: "ReportItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItem_ReportItem_ReportItemId",
                table: "CostItem",
                column: "ReportItemId",
                principalTable: "ReportItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CostItemReportItem_CostItem__costItemsLabel__costItemsCapab~",
                table: "CostItemReportItem",
                columns: new[] { "_costItemsLabel", "_costItemsCapabilityIdentifier" },
                principalTable: "CostItem",
                principalColumns: new[] { "Label", "CapabilityIdentifier" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItem_ReportItem_ReportItemId",
                table: "CostItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CostItemReportItem_CostItem__costItemsLabel__costItemsCapab~",
                table: "CostItemReportItem");

            migrationBuilder.DropIndex(
                name: "IX_CostItem_ReportItemId",
                table: "CostItem");

            migrationBuilder.DropColumn(
                name: "ReportItemId",
                table: "CostItem");

            migrationBuilder.RenameColumn(
                name: "_costItemsCapabilityIdentifier",
                table: "CostItemReportItem",
                newName: "CostItemsCapabilityIdentifier");

            migrationBuilder.RenameColumn(
                name: "_costItemsLabel",
                table: "CostItemReportItem",
                newName: "CostItemsLabel");

            migrationBuilder.RenameIndex(
                name: "IX_CostItemReportItem__costItemsLabel__costItemsCapabilityIden~",
                table: "CostItemReportItem",
                newName: "IX_CostItemReportItem_CostItemsLabel_CostItemsCapabilityIdenti~");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItemReportItem_CostItem_CostItemsLabel_CostItemsCapabil~",
                table: "CostItemReportItem",
                columns: new[] { "CostItemsLabel", "CostItemsCapabilityIdentifier" },
                principalTable: "CostItem",
                principalColumns: new[] { "Label", "CapabilityIdentifier" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}

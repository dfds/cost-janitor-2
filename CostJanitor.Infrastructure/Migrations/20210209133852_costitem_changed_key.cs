using Microsoft.EntityFrameworkCore.Migrations;

namespace CostJanitor.Infrastructure.Migrations
{
    public partial class costitem_changed_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItemReportItem_CostItem_CostItemsLabel_CostItemsCapabil~",
                table: "CostItemReportItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostItemReportItem",
                table: "CostItemReportItem");

            migrationBuilder.DropIndex(
                name: "IX_CostItemReportItem_CostItemsLabel_CostItemsCapabilityIdenti~",
                table: "CostItemReportItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostItem",
                table: "CostItem");

            migrationBuilder.DropColumn(
                name: "CostItemsCapabilityIdentifier",
                table: "CostItemReportItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostItemReportItem",
                table: "CostItemReportItem",
                columns: new[] { "CostItemsId", "CostItemsLabel" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostItem",
                table: "CostItem",
                column: "Label");

            migrationBuilder.CreateIndex(
                name: "IX_CostItemReportItem_CostItemsLabel",
                table: "CostItemReportItem",
                column: "CostItemsLabel");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItemReportItem_CostItem_CostItemsLabel",
                table: "CostItemReportItem",
                column: "CostItemsLabel",
                principalTable: "CostItem",
                principalColumn: "Label",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItemReportItem_CostItem_CostItemsLabel",
                table: "CostItemReportItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostItemReportItem",
                table: "CostItemReportItem");

            migrationBuilder.DropIndex(
                name: "IX_CostItemReportItem_CostItemsLabel",
                table: "CostItemReportItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostItem",
                table: "CostItem");

            migrationBuilder.AddColumn<string>(
                name: "CostItemsCapabilityIdentifier",
                table: "CostItemReportItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostItemReportItem",
                table: "CostItemReportItem",
                columns: new[] { "CostItemsId", "CostItemsLabel", "CostItemsCapabilityIdentifier" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostItem",
                table: "CostItem",
                columns: new[] { "Label", "CapabilityIdentifier" });

            migrationBuilder.CreateIndex(
                name: "IX_CostItemReportItem_CostItemsLabel_CostItemsCapabilityIdenti~",
                table: "CostItemReportItem",
                columns: new[] { "CostItemsLabel", "CostItemsCapabilityIdentifier" });

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

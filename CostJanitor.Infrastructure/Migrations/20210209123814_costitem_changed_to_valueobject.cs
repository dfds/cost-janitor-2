using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostJanitor.Infrastructure.Migrations
{
    public partial class costitem_changed_to_valueobject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostItem",
                columns: table => new
                {
                    Label = table.Column<string>(type: "text", nullable: false),
                    CapabilityIdentifier = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostItem", x => new { x.Label, x.CapabilityIdentifier });
                });

            migrationBuilder.CreateTable(
                name: "ReportItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostItemReportItem",
                columns: table => new
                {
                    CostItemsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CostItemsLabel = table.Column<string>(type: "text", nullable: false),
                    CostItemsCapabilityIdentifier = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostItemReportItem", x => new { x.CostItemsId, x.CostItemsLabel, x.CostItemsCapabilityIdentifier });
                    table.ForeignKey(
                        name: "FK_CostItemReportItem_CostItem_CostItemsLabel_CostItemsCapabil~",
                        columns: x => new { x.CostItemsLabel, x.CostItemsCapabilityIdentifier },
                        principalTable: "CostItem",
                        principalColumns: new[] { "Label", "CapabilityIdentifier" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostItemReportItem_ReportItem_CostItemsId",
                        column: x => x.CostItemsId,
                        principalTable: "ReportItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostItemReportItem_CostItemsLabel_CostItemsCapabilityIdenti~",
                table: "CostItemReportItem",
                columns: new[] { "CostItemsLabel", "CostItemsCapabilityIdentifier" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostItemReportItem");

            migrationBuilder.DropTable(
                name: "CostItem");

            migrationBuilder.DropTable(
                name: "ReportItem");
        }
    }
}

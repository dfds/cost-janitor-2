using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostJanitor.Infrastructure.Migrations
{
    public partial class added_costitemreference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostItemReferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Added = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReportItemId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostItemReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostItemReferences_ReportItem_ReportItemId",
                        column: x => x.ReportItemId,
                        principalTable: "ReportItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostItemReferences_ReportItemId",
                table: "CostItemReferences",
                column: "ReportItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostItemReferences");
        }
    }
}

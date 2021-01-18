using Microsoft.EntityFrameworkCore.Migrations;

namespace CostJanitor.Infrastructure.Migrations
{
    public partial class id_and_identifier_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CapabilityIdentifier",
                table: "CostItemReferences",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CapabilityIdentifier",
                table: "CostItem",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapabilityIdentifier",
                table: "CostItemReferences");

            migrationBuilder.DropColumn(
                name: "CapabilityIdentifier",
                table: "CostItem");
        }
    }
}

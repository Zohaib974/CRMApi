using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMEntities.Migrations
{
    public partial class addedCompanyd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Jobs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Events",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Contacts",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Contacts");
        }
    }
}

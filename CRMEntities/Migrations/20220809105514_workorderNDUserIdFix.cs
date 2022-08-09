using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMEntities.Migrations
{
    public partial class workorderNDUserIdFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JobId",
                table: "WorkOrders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReferenceTable",
                table: "WorkOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_JobId",
                table: "WorkOrders",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Jobs_JobId",
                table: "WorkOrders",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Jobs_JobId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_JobId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "ReferenceTable",
                table: "WorkOrders");
        }
    }
}

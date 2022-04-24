using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMEntities.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "656cf162-9d28-47a5-9fb5-20cf7bbc3591", "c0be6e24-44a5-492c-9182-825bda954f40", "SuperAdmin", "SUPERADMIN" },
                    { "f15e1aba-a2c6-4280-b113-c124aaf05e4d", "060dcff0-a1d8-4a06-8222-5254596e6929", "Administrator", "ADMINISTRATOR" },
                    { "dcd8405c-b687-4ea3-b74c-ea788f43c5d5", "b8a7dc84-8ed2-45fc-a95b-caa4aa3823e0", "Manager", "MANAGER" },
                    { "5ee799ed-bd4b-42b5-a785-447dc9316c87", "618d9dc6-04d8-4253-8f30-9306148c504a", "Employee", "EMPLOYEE" },
                    { "343eeb60-e427-4562-906d-2301047952f0", "46d96e4b-2702-40bd-8075-f196250680e2", "User", "USER" },
                    { "cf648d75-f40d-4645-943d-ffad1dcb7de3", "f92393dc-ca6d-4d7c-869a-1a5e3c1c589f", "Guest", "GUEST" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "343eeb60-e427-4562-906d-2301047952f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ee799ed-bd4b-42b5-a785-447dc9316c87");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "656cf162-9d28-47a5-9fb5-20cf7bbc3591");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf648d75-f40d-4645-943d-ffad1dcb7de3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcd8405c-b687-4ea3-b74c-ea788f43c5d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f15e1aba-a2c6-4280-b113-c124aaf05e4d");
        }
    }
}

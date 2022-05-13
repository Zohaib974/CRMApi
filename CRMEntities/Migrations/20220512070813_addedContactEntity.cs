using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMEntities.Migrations
{
    public partial class addedContactEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: 1L);

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: 2L);

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: 3L);

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: 4L);

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: 5L);

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: 6L);

            //migrationBuilder.DeleteData(
            //    table: "Employees",
            //    keyColumn: "EmployeeId",
            //    keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            //migrationBuilder.DeleteData(
            //    table: "Employees",
            //    keyColumn: "EmployeeId",
            //    keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            //migrationBuilder.DeleteData(
            //    table: "Employees",
            //    keyColumn: "EmployeeId",
            //    keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            //migrationBuilder.DeleteData(
            //    table: "Companies",
            //    keyColumn: "CompanyId",
            //    keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            //migrationBuilder.DeleteData(
            //    table: "Companies",
            //    keyColumn: "CompanyId",
            //    keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastStatusChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastContacted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactedCount = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "IsPublic", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { 1L, "c4a9317d-ae05-4b6c-959b-c0093c15cccc", false, "SuperAdmin", "SUPERADMIN" },
            //        { 2L, "a037b98c-b23d-468c-8d66-1840be92dc4c", false, "Administrator", "ADMINISTRATOR" },
            //        { 3L, "0a663a09-7195-46bd-abb2-68029772c642", false, "Manager", "MANAGER" },
            //        { 4L, "921aeb80-9432-4d3f-b945-d4134bb3e845", false, "Employee", "EMPLOYEE" },
            //        { 5L, "86f4f218-485f-4755-9029-879a9bc347a4", false, "User", "USER" },
            //        { 6L, "25e124f6-9f2e-4875-9034-7bd87b7e5d76", false, "Guest", "GUEST" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Companies",
            //    columns: new[] { "CompanyId", "Address", "Country", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" },
            //        { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Employees",
            //    columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
            //    values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 26, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sam Raiden", "Software developer" });

            //migrationBuilder.InsertData(
            //    table: "Employees",
            //    columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
            //    values: new object[] { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 30, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Jana McLeaf", "Software developer" });

            //migrationBuilder.InsertData(
            //    table: "Employees",
            //    columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
            //    values: new object[] { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), 35, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Kane Miller", "Administrator" });
        }
    }
}

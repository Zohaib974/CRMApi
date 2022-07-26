using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMEntities.Migrations
{
    public partial class manyToManyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventContacts",
                columns: table => new
                {
                    ContactId = table.Column<long>(type: "bigint", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventContacts", x => new { x.EventId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_EventContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventContacts_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobContacts",
                columns: table => new
                {
                    ContactId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContacts", x => new { x.JobId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_JobContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobContacts_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventContacts_ContactId",
                table: "EventContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContacts_ContactId",
                table: "JobContacts",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventContacts");

            migrationBuilder.DropTable(
                name: "JobContacts");
        }
    }
}

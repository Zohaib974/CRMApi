using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMEntities.Migrations
{
    public partial class addRelCtc2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatedContacts",
                columns: table => new
                {
                    ContactId = table.Column<long>(type: "bigint", nullable: false),
                    RelContactId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedContacts", x => new { x.RelContactId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_RelatedContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RelatedContacts_Contacts_RelContactId",
                        column: x => x.RelContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedContacts_ContactId",
                table: "RelatedContacts",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedContacts");
        }
    }
}

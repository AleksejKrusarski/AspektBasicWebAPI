using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspektBasicWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CountryId",
                table: "Contacts",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Companys_CompanyId",
                table: "Contacts",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Countrys_CountryId",
                table: "Contacts",
                column: "CountryId",
                principalTable: "Countrys",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Companys_CompanyId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Countrys_CountryId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CompanyId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CountryId",
                table: "Contacts");
        }
    }
}

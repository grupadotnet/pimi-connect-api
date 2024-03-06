using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pimi_connect_api.Migrations
{
    /// <inheritdoc />
    public partial class RenamedFieldInAttachmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "publicName",
                table: "Attachments",
                newName: "PublicName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicName",
                table: "Attachments",
                newName: "publicName");
        }
    }
}

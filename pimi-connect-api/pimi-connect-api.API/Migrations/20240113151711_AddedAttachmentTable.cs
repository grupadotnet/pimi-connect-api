using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pimi_connect_api.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttachmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentEntity_Messages_MessageEntityId",
                table: "AttachmentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AttachmentEntity_ThumbnailId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AttachmentEntity_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttachmentEntity",
                table: "AttachmentEntity");

            migrationBuilder.RenameTable(
                name: "AttachmentEntity",
                newName: "Attachments");

            migrationBuilder.RenameIndex(
                name: "IX_AttachmentEntity_MessageEntityId",
                table: "Attachments",
                newName: "IX_Attachments_MessageEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Messages_MessageEntityId",
                table: "Attachments",
                column: "MessageEntityId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Attachments_ThumbnailId",
                table: "Chats",
                column: "ThumbnailId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Messages_MessageEntityId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Attachments_ThumbnailId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "AttachmentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_MessageEntityId",
                table: "AttachmentEntity",
                newName: "IX_AttachmentEntity_MessageEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttachmentEntity",
                table: "AttachmentEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentEntity_Messages_MessageEntityId",
                table: "AttachmentEntity",
                column: "MessageEntityId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AttachmentEntity_ThumbnailId",
                table: "Chats",
                column: "ThumbnailId",
                principalTable: "AttachmentEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AttachmentEntity_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "AttachmentEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

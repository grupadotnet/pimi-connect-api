using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pimi_connect_api.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatThumbnails_ThumbnailId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePictures_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ChatThumbnails");

            migrationBuilder.DropTable(
                name: "MessageAttachments");

            migrationBuilder.DropTable(
                name: "ProfilePictures");

            migrationBuilder.CreateTable(
                name: "AttachmentEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    publicName = table.Column<string>(type: "text", nullable: false),
                    MessageEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttachmentEntity_Messages_MessageEntityId",
                        column: x => x.MessageEntityId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentEntity_MessageEntityId",
                table: "AttachmentEntity",
                column: "MessageEntityId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AttachmentEntity_ThumbnailId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AttachmentEntity_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AttachmentEntity");

            migrationBuilder.CreateTable(
                name: "ChatThumbnails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    publicName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatThumbnails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    publicName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachments_Messages_MessageEntityId",
                        column: x => x.MessageEntityId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    publicName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachments_MessageEntityId",
                table: "MessageAttachments",
                column: "MessageEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_ChatThumbnails_ThumbnailId",
                table: "Chats",
                column: "ThumbnailId",
                principalTable: "ChatThumbnails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePictures_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "ProfilePictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

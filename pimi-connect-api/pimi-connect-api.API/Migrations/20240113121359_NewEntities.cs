using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pimi_connect_api.Migrations
{
    /// <inheritdoc />
    public partial class NewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Attachments_ThumbnailId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthEntities_AuthId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthEntities",
                table: "AuthEntities");

            migrationBuilder.DropColumn(
                name: "ProfilePictureKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ThumbnailKey",
                table: "Chats");

            migrationBuilder.RenameTable(
                name: "AuthEntities",
                newName: "Auths");

            migrationBuilder.RenameColumn(
                name: "IdPasswordContainer",
                table: "Messages",
                newName: "PasswordContainerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ThumbnailId",
                table: "Chats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PrivateKey",
                table: "Auths",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auths",
                table: "Auths",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChatPasswords",
                columns: table => new
                {
                    PasswordContainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ChatThumbnails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    publicName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatThumbnails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Template = table.Column<int>(type: "integer", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
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
                name: "PasswordContainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordContainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    publicName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IndirectKey = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKeys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PasswordContainerId",
                table: "Messages",
                column: "PasswordContainerId");

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
                name: "FK_Messages_PasswordContainers_PasswordContainerId",
                table: "Messages",
                column: "PasswordContainerId",
                principalTable: "PasswordContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Auths_AuthId",
                table: "Users",
                column: "AuthId",
                principalTable: "Auths",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePictures_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "ProfilePictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatThumbnails_ThumbnailId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_PasswordContainers_PasswordContainerId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Auths_AuthId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePictures_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ChatPasswords");

            migrationBuilder.DropTable(
                name: "ChatThumbnails");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "MessageAttachments");

            migrationBuilder.DropTable(
                name: "PasswordContainers");

            migrationBuilder.DropTable(
                name: "ProfilePictures");

            migrationBuilder.DropTable(
                name: "UserKeys");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PasswordContainerId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auths",
                table: "Auths");

            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "Auths");

            migrationBuilder.RenameTable(
                name: "Auths",
                newName: "AuthEntities");

            migrationBuilder.RenameColumn(
                name: "PasswordContainerId",
                table: "Messages",
                newName: "IdPasswordContainer");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureKey",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ThumbnailId",
                table: "Chats",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailKey",
                table: "Chats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthEntities",
                table: "AuthEntities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Messages_MessageEntityId",
                        column: x => x.MessageEntityId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_MessageEntityId",
                table: "Attachments",
                column: "MessageEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Attachments_ThumbnailId",
                table: "Chats",
                column: "ThumbnailId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AuthEntities_AuthId",
                table: "Users",
                column: "AuthId",
                principalTable: "AuthEntities",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pimi_connect_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAuthEntityForUserToBeOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthEntities_AuthId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthId",
                table: "Users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_ProfilePictureId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthEntities_AuthId",
                table: "Users");

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
                name: "AuthId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AuthEntities_AuthId",
                table: "Users",
                column: "AuthId",
                principalTable: "AuthEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

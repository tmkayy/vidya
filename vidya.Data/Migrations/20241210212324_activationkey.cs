using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vidya.Data.Migrations
{
    /// <inheritdoc />
    public partial class activationkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivationKeys_AspNetUsers_ApplicationUserId",
                table: "ActivationKeys");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ActivationKeys",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivationKeys_ApplicationUserId",
                table: "ActivationKeys",
                newName: "IX_ActivationKeys_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivationKeys_AspNetUsers_UserId",
                table: "ActivationKeys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivationKeys_AspNetUsers_UserId",
                table: "ActivationKeys");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ActivationKeys",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivationKeys_UserId",
                table: "ActivationKeys",
                newName: "IX_ActivationKeys_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivationKeys_AspNetUsers_ApplicationUserId",
                table: "ActivationKeys",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

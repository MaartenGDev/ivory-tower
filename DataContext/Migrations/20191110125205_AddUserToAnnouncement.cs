using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class AddUserToAnnouncement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Announcements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_UserId",
                table: "Announcements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AspNetUsers_UserId",
                table: "Announcements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AspNetUsers_UserId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_UserId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Announcements");
        }
    }
}

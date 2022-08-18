using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Repository.Migrations
{
    public partial class ds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCarRental_Cars_CarId",
                table: "UserCarRental");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCarRental_Users_UserId",
                table: "UserCarRental");

            migrationBuilder.DropIndex(
                name: "IX_UserCarRental_CarId",
                table: "UserCarRental");

            migrationBuilder.DropIndex(
                name: "IX_UserCarRental_UserId",
                table: "UserCarRental");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserCarRental_CarId",
                table: "UserCarRental",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCarRental_UserId",
                table: "UserCarRental",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCarRental_Cars_CarId",
                table: "UserCarRental",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCarRental_Users_UserId",
                table: "UserCarRental",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

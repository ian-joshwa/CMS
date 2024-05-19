using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.DataAccessLayer.Migrations
{
    public partial class course_new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SessionId",
                table: "Courses",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Sessions_SessionId",
                table: "Courses",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Sessions_SessionId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SessionId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Courses");
        }
    }
}

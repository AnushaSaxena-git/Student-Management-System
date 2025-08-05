using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crudbyme.Migrations
{
    /// <inheritdoc />
    public partial class CRUDBYME : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studentage",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseIds",
                table: "Students",
                column: "CourseIds");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CourseIds",
                table: "Students",
                column: "CourseIds",
                principalTable: "Courses",
                principalColumn: "CourseIds",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CourseIds",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CourseIds",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "studentage",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

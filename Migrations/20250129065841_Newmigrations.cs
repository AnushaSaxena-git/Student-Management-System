using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crudbyme.Migrations
{
    /// <inheritdoc />
    public partial class Newmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "studentage",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studentage",
                table: "Students");
        }
    }
}

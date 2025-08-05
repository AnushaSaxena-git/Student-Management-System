using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crudbyme.Migrations
{
    /// <inheritdoc />
    public partial class CRUDBYMENEW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studentage",
                table: "Students");

            migrationBuilder.AddColumn<DateTime>(
                name: "studentDateOfBirth",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 1,
                column: "DeptId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 2,
                column: "DeptId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 3,
                column: "DeptId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 4,
                column: "DeptId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 5,
                column: "DeptId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 6,
                column: "DeptId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studentDateOfBirth",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "studentage",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 1,
                column: "DeptId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 2,
                column: "DeptId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 3,
                column: "DeptId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 4,
                column: "DeptId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 5,
                column: "DeptId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseIds",
                keyValue: 6,
                column: "DeptId",
                value: 1);
        }
    }
}

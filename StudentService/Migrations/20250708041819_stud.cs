using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class stud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stud",
                columns: table => new
                {
                    studId = table.Column<int>(type: "int", nullable: false),
                    studName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    studTotalMarks = table.Column<int>(type: "int", nullable: false),
                    studDOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studGender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stud", x => x.studId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stud");
        }
    }
}

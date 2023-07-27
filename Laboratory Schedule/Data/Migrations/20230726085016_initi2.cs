using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratory_Schedule.Data.Migrations
{
    public partial class initi2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mangement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalResidenceId = table.Column<int>(type: "int", nullable: false),
                    UniversityNumber = table.Column<int>(type: "int", nullable: false),
                    StudentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Collage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameEng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherNameEng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherNameEng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNameEng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameArb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherNameArb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherNameArb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNameArb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalFileNO = table.Column<int>(type: "int", nullable: false),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mangement");

            migrationBuilder.DropTable(
                name: "Request");
        }
    }
}

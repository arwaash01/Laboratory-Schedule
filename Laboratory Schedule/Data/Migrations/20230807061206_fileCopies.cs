using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratory_Schedule.Data.Migrations
{
    public partial class fileCopies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalResidenceIdName",
                table: "Request",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentIdName",
                table: "Request",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalResidenceIdName",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "StudentIdName",
                table: "Request");
        }
    }
}

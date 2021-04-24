using Microsoft.EntityFrameworkCore.Migrations;

namespace OwnThings.Core.Migrations
{
    public partial class MeasurementUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "measurements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "owner",
                table: "measurements");
        }
    }
}

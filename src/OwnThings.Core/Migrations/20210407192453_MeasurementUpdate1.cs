using Microsoft.EntityFrameworkCore.Migrations;

namespace OwnThings.Core.Migrations
{
    public partial class MeasurementUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "owner",
                table: "measurements");

            migrationBuilder.AddColumn<long>(
                name: "ownerid",
                table: "measurements",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_measurements_ownerid",
                table: "measurements",
                column: "ownerid");

            migrationBuilder.AddForeignKey(
                name: "FK_measurements_users_ownerid",
                table: "measurements",
                column: "ownerid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_measurements_users_ownerid",
                table: "measurements");

            migrationBuilder.DropIndex(
                name: "IX_measurements_ownerid",
                table: "measurements");

            migrationBuilder.DropColumn(
                name: "ownerid",
                table: "measurements");

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "measurements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace OwnThings.Core.Migrations
{
    public partial class MeasurementUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_measurements_users_ownerid",
                table: "measurements");

            migrationBuilder.AlterColumn<long>(
                name: "ownerid",
                table: "measurements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_measurements_users_ownerid",
                table: "measurements",
                column: "ownerid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_measurements_users_ownerid",
                table: "measurements");

            migrationBuilder.AlterColumn<long>(
                name: "ownerid",
                table: "measurements",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_measurements_users_ownerid",
                table: "measurements",
                column: "ownerid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

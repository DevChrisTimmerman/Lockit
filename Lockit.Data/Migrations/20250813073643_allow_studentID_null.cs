using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lockit.Data.Migrations
{
    /// <inheritdoc />
    public partial class allow_studentID_null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lockers_Students_StudentID",
                table: "Lockers");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "Lockers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Lockers_Students_StudentID",
                table: "Lockers",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lockers_Students_StudentID",
                table: "Lockers");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "Lockers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lockers_Students_StudentID",
                table: "Lockers",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

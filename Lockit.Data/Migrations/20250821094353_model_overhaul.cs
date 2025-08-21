using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lockit.Data.Migrations
{
    /// <inheritdoc />
    public partial class model_overhaul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lockers_StudentID",
                table: "Lockers");

            migrationBuilder.AddColumn<string>(
                name: "SCUID",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LockerCount",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lockers_StudentID",
                table: "Lockers",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lockers_StudentID",
                table: "Lockers");

            migrationBuilder.DropColumn(
                name: "SCUID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LockerCount",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_Lockers_StudentID",
                table: "Lockers",
                column: "StudentID");
        }
    }
}

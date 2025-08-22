using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lockit.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixed_migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lockers_StudentID",
                table: "Lockers");

            migrationBuilder.CreateIndex(
                name: "IX_Lockers_StudentID",
                table: "Lockers",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lockers_StudentID",
                table: "Lockers");

            migrationBuilder.CreateIndex(
                name: "IX_Lockers_StudentID",
                table: "Lockers",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");
        }
    }
}

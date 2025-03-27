using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1_lab_BD_tran.Migrations
{
    /// <inheritdoc />
    public partial class TwoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "title_Index",
                table: "tags",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "title_Index",
                table: "tags");
        }
    }
}

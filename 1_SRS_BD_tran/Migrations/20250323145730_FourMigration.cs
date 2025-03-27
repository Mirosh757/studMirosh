using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1_lab_BD_tran.Migrations
{
    /// <inheritdoc />
    public partial class FourMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[]>(
                name: "tags",
                table: "accounts",
                type: "integer[]",
                nullable: true,
                defaultValue: new[] { -1 },
                oldClrType: typeof(int[]),
                oldType: "integer[]",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[]>(
                name: "tags",
                table: "accounts",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(int[]),
                oldType: "integer[]",
                oldNullable: true,
                oldDefaultValue: new[] { -1 });
        }
    }
}

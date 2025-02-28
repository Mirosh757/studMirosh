using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace _1_lab_BD_tran.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    registration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    family = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("accounts_pkey", x => x.user_id);
                    table.ForeignKey(
                        name: "user_id_foreign",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "login_Index",
                table: "users",
                column: "login",
                unique: true);
            migrationBuilder.Sql(@"
                START TRANSACTION;
                CREATE OR REPLACE PROCEDURE add_user(login_value VARCHAR(50), password_value VARCHAR(50))
                LANGUAGE sql
                AS $$
		                INSERT INTO users(login, password, registration_date) VALUES (login_value, password_value, current_timestamp);
                $$;
                COMMIT;

                START TRANSACTION;
                CREATE OR REPLACE PROCEDURE add_account(user_id_value INT, family_value VARCHAR(50), name_value VARCHAR(50), patronymic_value VARCHAR(50), birth_date_value DATE)
                LANGUAGE sql
                AS $$
		                INSERT INTO accounts(user_id, family, name, patronymic, birth_date) VALUES (user_id_value, family_value, name_value, patronymic_value, birth_date_value);
                $$;
                COMMIT;                   
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

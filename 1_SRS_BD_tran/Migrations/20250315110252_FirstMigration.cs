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
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

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
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tags = table.Column<int[]>(type: "integer[]", nullable: false)
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
                CREATE OR REPLACE PROCEDURE add_user(login_value VARCHAR(50), password_value VARCHAR(50), family_value VARCHAR(50), name_value VARCHAR(50), patronymic_value VARCHAR(50), birth_date_value DATE)
                LANGUAGE sql
                AS $$
                        INSERT INTO users(login, password, registration_date) VALUES (login_value, password_value, current_timestamp);
                        INSERT INTO accounts(user_id, family, name, patronymic, birth_date) VALUES ((SELECT id FROM users WHERE users.login = login_value), family_value, name_value, patronymic_value, birth_date_value);
                $$;
                COMMIT;



                CREATE OR REPLACE FUNCTION public.checktag(
	            title_value TEXT)
                RETURNS integer
                LANGUAGE 'plpgsql'
                COST 100
                VOLATILE PARALLEL UNSAFE
                AS $BODY$
                 << outerblock >>
                 DECLARE 
                 --checkhelp bool := (SELECT CAST( CASE WHEN EXISTS (SELECT 1 FROM tags WHERE tags.title = title_value) THEN 1 ELSE 0 END AS BIT));
                 test_val integer := 0;
                 BEGIN
	                IF ((SELECT COUNT(*) FROM tags WHERE tags.title = title_value) = 0) THEN
		                INSERT INTO tags(title) VALUES (title_value);
	                END IF;
	                test_val := (SELECT id FROM tags WHERE tags.title = title_value LIMIT 1);
                 RETURN test_val;
                 END;
                $BODY$;
                ALTER FUNCTION public.checktag(character varying)
                    OWNER TO postgres;




                CREATE OR REPLACE FUNCTION public.addtagforuser(
	            id_value integer,
	            title_value TEXT)
                RETURNS text
                LANGUAGE 'plpgsql'
                COST 100
                VOLATILE PARALLEL UNSAFE
                AS $BODY$
                             << outerblock >>
                             DECLARE 
				             checktag_value INT = checktag(title_value);
                             checkhelp bool := (SELECT CAST( CASE WHEN EXISTS (SELECT 1 FROM accounts WHERE checktag_value = ANY(accounts.tags)) THEN 1 ELSE 0 END AS BIT));
                             test_return TEXT := '';
                             BEGIN
                                IF (checkhelp = false) THEN
                                    UPDATE accounts SET tags = ARRAY_APPEND(tags, checktag_value) WHERE user_id = id_value;
		                            test_return := 'Вставка тэга прошла успешно';
	                            ELSE
		                            test_return := 'Данный тэг уже был вставлен';
                                END IF;
                             RETURN test_return;
                             END;
            $BODY$;

            ALTER FUNCTION public.addtagforuser(integer, character varying)
                OWNER TO postgres;




                CREATE OR REPLACE FUNCTION deletetagwithuser(id_account_value INT, id_tag_value INT) RETURNS TEXT AS $$
                 << outerblock >>
                 DECLARE 
                 checkhelp bool := (SELECT CAST( CASE WHEN EXISTS (SELECT 1 FROM accounts WHERE id_tag_value = ANY(accounts.tags)) THEN 1 ELSE 0 END AS BIT));
                 test_return TEXT := '';
                 BEGIN
                    IF (checkhelp = TRUE) THEN
                        UPDATE accounts SET tags = ARRAY_REMOVE(tags, id_tag_value) WHERE user_id = id_account_value;
                        test_return := 'Удаление тэга с аккаунта прошло успешно';
                    ELSE
                        test_return := 'Тэга с указанным id не указанно у пользователя';
                    END IF;
                 RETURN test_return;
                 END;
                 $$ LANGUAGE plpgsql;



                CREATE OR REPLACE FUNCTION public.deletetag(id_value INT) RETURNS text
                    LANGUAGE 'plpgsql'
                    COST 100
                    VOLATILE PARALLEL UNSAFE
                AS $BODY$
                 << outerblock >>
                 DECLARE 
                 checkhelp bool := (SELECT CAST( CASE WHEN EXISTS (SELECT 1 FROM accounts WHERE id_value = ANY(accounts.tags)) THEN 1 ELSE 0 END AS BIT));
                 test_return TEXT := '';
                 BEGIN
                    IF (checkhelp = TRUE) THEN
		                DELETE FROM tags WHERE id = id_value;
                        UPDATE accounts SET tags = ARRAY_REMOVE(tags, id_value) WHERE id_value = ANY(accounts.tags);
                        test_return := 'Удаление тэга прошло успешно';
                    ELSE
                        test_return := 'Данный тэг не существует';
                    END IF;
                 RETURN test_return;
                 END;
 
                $BODY$;

                ALTER FUNCTION public.deletetag(integer)
                    OWNER TO postgres;

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

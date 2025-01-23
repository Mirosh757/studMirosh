using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace _6_lab_NoPattern.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    passport_details = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    date_birth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("doctors_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "general_pages",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    website = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("general_pages_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    region_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("regions_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specialities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    title = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("specialities_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    general_page_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("addresses_pkey", x => x.id);
                    table.ForeignKey(
                        name: "addresses_general_page_id_foreign",
                        column: x => x.general_page_id,
                        principalTable: "general_pages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phone_numbers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    contact_phone_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    general_page_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("phone_numbers_pkey", x => x.id);
                    table.ForeignKey(
                        name: "phone_numbers_general_page_id_foreign",
                        column: x => x.general_page_id,
                        principalTable: "general_pages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hospitals",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    region_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("hospitals_pkey", x => x.id);
                    table.ForeignKey(
                        name: "hospitals_id_foreign",
                        column: x => x.id,
                        principalTable: "general_pages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "hospitals_region_id_foreign",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    hospital_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("departments_pkey", x => x.id);
                    table.ForeignKey(
                        name: "departments_hospital_id_foreign",
                        column: x => x.hospital_id,
                        principalTable: "hospitals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "departments_id_foreign",
                        column: x => x.id,
                        principalTable: "general_pages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requisites",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    registration_date = table.Column<DateOnly>(type: "date", nullable: false),
                    hospital_reduce_name = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    name_legal_faces = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ogrn = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    inn = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    kpp = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    hospital_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("requisites_pkey", x => x.id);
                    table.ForeignKey(
                        name: "requisites_hospital_id_foreign",
                        column: x => x.hospital_id,
                        principalTable: "hospitals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "doctor_specialities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: true),
                    date_start = table.Column<DateOnly>(type: "date", nullable: false),
                    date_end = table.Column<DateOnly>(type: "date", nullable: true),
                    speciality_id = table.Column<long>(type: "bigint", nullable: false),
                    doctor_id = table.Column<long>(type: "bigint", nullable: false),
                    department_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("doctor_specialities_pkey", x => x.id);
                    table.ForeignKey(
                        name: "doctor_specialities_department_id_foreign",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "doctor_specialities_doctor_id_foreign",
                        column: x => x.doctor_id,
                        principalTable: "doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "doctor_specialities_speciality_id_foreign",
                        column: x => x.speciality_id,
                        principalTable: "specialities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_general_page_id",
                table: "addresses",
                column: "general_page_id");

            migrationBuilder.CreateIndex(
                name: "IX_departments_hospital_id",
                table: "departments",
                column: "hospital_id");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_specialities_department_id",
                table: "doctor_specialities",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_specialities_doctor_id",
                table: "doctor_specialities",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_specialities_speciality_id",
                table: "doctor_specialities",
                column: "speciality_id");

            migrationBuilder.CreateIndex(
                name: "doctors_passport_details_unique",
                table: "doctors",
                column: "passport_details",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hospitals_region_id",
                table: "hospitals",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_phone_numbers_general_page_id",
                table: "phone_numbers",
                column: "general_page_id");

            migrationBuilder.CreateIndex(
                name: "IX_requisites_hospital_id",
                table: "requisites",
                column: "hospital_id");

            migrationBuilder.CreateIndex(
                name: "requisites_hospital_reduce_name_unique",
                table: "requisites",
                column: "hospital_reduce_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "requisites_inn_unique",
                table: "requisites",
                column: "inn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "requisites_kpp_unique",
                table: "requisites",
                column: "kpp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "requisites_name_legal_faces_unique",
                table: "requisites",
                column: "name_legal_faces",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "requisites_ogrn_unique",
                table: "requisites",
                column: "ogrn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "requisites_registration_date_unique",
                table: "requisites",
                column: "registration_date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "specialities_title_unique",
                table: "specialities",
                column: "title",
                unique: true);
            using (DoctorsContext db = new DoctorsContext())
            {
                int viewLite = db.Database.ExecuteSqlRaw(@"
                     CREATE OR REPLACE VIEW viewhospitaladressesandphonenumber
                     AS
                     SELECT requisites.hospital_reduce_name,
                        addresses.address,
                        phone_numbers.contact_phone_number
                       FROM general_pages
                         JOIN hospitals ON general_pages.id = hospitals.id
                         JOIN requisites ON general_pages.id = requisites.hospital_id
                         JOIN addresses ON general_pages.id = addresses.general_page_id
                         JOIN phone_numbers ON general_pages.id = phone_numbers.general_page_id
                      ORDER BY requisites.hospital_reduce_name;


                    CREATE MATERIALIZED VIEW ViewForDoctors AS
                    SELECT 
		                    doctors.name,
		                    specialities.title,
		                    COUNT(specialities.title) OVER (
			                    PARTITION BY doctors.name
			                    ORDER BY doctors.name
		                    )
                    FROM doctors
                    INNER JOIN doctor_specialities ON doctor_specialities.doctor_id = doctors.id 
                    INNER JOIN specialities ON doctor_specialities.speciality_id = specialities.id;
                    ");
                            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "doctor_specialities");

            migrationBuilder.DropTable(
                name: "phone_numbers");

            migrationBuilder.DropTable(
                name: "requisites");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "specialities");

            migrationBuilder.DropTable(
                name: "hospitals");

            migrationBuilder.DropTable(
                name: "general_pages");

            migrationBuilder.DropTable(
                name: "regions");
        }
    }
}

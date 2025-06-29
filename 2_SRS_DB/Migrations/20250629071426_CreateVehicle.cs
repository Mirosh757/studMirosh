using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace _2_SRS_DB.Migrations
{
    /// <inheritdoc />
    public partial class CreateVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    passenger_capacity = table.Column<int>(type: "integer", nullable: false),
                    body_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_vehicles_Id",
                        column: x => x.Id,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "motorcycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    engine_volume = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    is_racing = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motorcycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_motorcycles_vehicles_Id",
                        column: x => x.Id,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    load_capacity = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    axle_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trucks_vehicles_Id",
                        column: x => x.Id,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.Sql(@"
                    INSERT INTO cars (brand, model, year, passenger_capacity, body_type)
                    VALUES 
                        ('Toyota', 'Camry', 2020, 5, 'Sedan'),
                        ('Honda', 'Civic', 2021, 5, 'Hatchback'),
                        ('Ford', 'Focus', 2019, 5, 'Sedan'),
                        ('BMW', 'X5', 2022, 5, 'SUV'),
                        ('Audi', 'A4', 2021, 5, 'Sedan'),
                        ('Mercedes', 'C-Class', 2020, 5, 'Coupe'),
                        ('Volkswagen', 'Golf', 2018, 5, 'Hatchback'),
                        ('Hyundai', 'Tucson', 2021, 5, 'SUV'),
                        ('Kia', 'Rio', 2020, 5, 'Sedan'),
                        ('Lexus', 'RX', 2022, 5, 'SUV');

                    INSERT INTO trucks (brand, model, year, load_capacity, axle_count)
                    VALUES 
                        ('Volvo', 'FH16', 2020, 20.5, 3),
                        ('MAN', 'TGX', 2021, 18.0, 3),
                        ('Scania', 'R500', 2019, 22.0, 4),
                        ('Mercedes', 'Actros', 2022, 19.5, 3),
                        ('DAF', 'XF', 2021, 20.0, 3),
                        ('Iveco', 'Hi-Way', 2020, 18.5, 3),
                        ('Kenworth', 'T680', 2018, 25.0, 4),
                        ('Peterbilt', '579', 2021, 24.5, 4),
                        ('Freightliner', 'Cascadia', 2020, 23.0, 4),
                        ('Mack', 'Anthem', 2022, 21.5, 3);

                    INSERT INTO motorcycles (brand, model, year, engine_volume, is_racing)
                    VALUES 
                        ('Harley-Davidson', 'Sportster', 2020, 883.0, FALSE),
                        ('Yamaha', 'YZF-R1', 2021, 998.0, TRUE),
                        ('Honda', 'CBR600RR', 2019, 599.0, TRUE),
                        ('Kawasaki', 'Ninja 650', 2022, 649.0, FALSE),
                        ('Ducati', 'Panigale V4', 2021, 703.0, TRUE),
                        ('BMW', 'S1000RR', 2020, 999.0, TRUE),
                        ('Suzuki', 'GSX-R750', 2018, 750.0, TRUE),
                        ('Triumph', 'Bonneville', 2021, 800.0, FALSE),
                        ('KTM', 'Duke 390', 2020, 373.0, FALSE),
                        ('Aprilia', 'RSV4', 2022, 799.0, TRUE);
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "motorcycles");

            migrationBuilder.DropTable(
                name: "trucks");

            migrationBuilder.DropTable(
                name: "vehicles");
        }
    }
}

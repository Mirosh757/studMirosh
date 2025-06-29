using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace _2_SRS_DB
{
    internal class VehicleRepository
    {
        private readonly string _connectionString;

        public VehicleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public VehicleRepository() 
        {
            _connectionString = "Host = localhost; Username = postgres; Password = 5dartyr5; Database = Vehicle";
        }

        // CRUD операции для Car
        public void AddCar(Car car)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "INSERT INTO cars (brand, model, year, passenger_capacity, body_type) " +
                    "VALUES (@brand, @model, @year, @passengerCapacity, @bodyType)", conn))
                {
                    cmd.Parameters.AddWithValue("@brand", car.Brand);
                    cmd.Parameters.AddWithValue("@model", car.Model);
                    cmd.Parameters.AddWithValue("@year", car.Year);
                    cmd.Parameters.AddWithValue("@passengerCapacity", car.PassengerCapacity);
                    cmd.Parameters.AddWithValue("@bodyType", car.BodyType);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Car> GetAllCars()
        {
            var cars = new List<Car>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM cars", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cars.Add(new Car
                        {
                            Id = reader.GetInt32(0),
                            Brand = reader.GetString(1),
                            Model = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            CreatedAt = reader.GetDateTime(4),
                            PassengerCapacity = reader.GetInt32(5),
                            BodyType = reader.GetString(6)
                        });
                    }
                }
            }
            return cars;
        }

        public void UpdateCar(Car car)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "UPDATE cars SET brand = @brand, model = @model, year = @year, " +
                    "passenger_capacity = @passengerCapacity, body_type = @bodyType " +
                    "WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", car.Id);
                    cmd.Parameters.AddWithValue("@brand", car.Brand);
                    cmd.Parameters.AddWithValue("@model", car.Model);
                    cmd.Parameters.AddWithValue("@year", car.Year);
                    cmd.Parameters.AddWithValue("@passengerCapacity", car.PassengerCapacity);
                    cmd.Parameters.AddWithValue("@bodyType", car.BodyType);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCar(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM cars WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // CRUD операции для Truck (аналогично Car)
        public void AddTruck(Truck truck)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "INSERT INTO trucks (brand, model, year, load_capacity, axle_count) " +
                    "VALUES (@brand, @model, @year, @loadCapacity, @axleCount)", conn))
                {
                    cmd.Parameters.AddWithValue("@brand", truck.Brand);
                    cmd.Parameters.AddWithValue("@model", truck.Model);
                    cmd.Parameters.AddWithValue("@year", truck.Year);
                    cmd.Parameters.AddWithValue("@loadCapacity", truck.LoadCapacity);
                    cmd.Parameters.AddWithValue("@axleCount", truck.AxleCount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Truck> GetAllTrucks()
        {
            var trucks = new List<Truck>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM trucks", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trucks.Add(new Truck
                        {
                            Id = reader.GetInt32(0),
                            Brand = reader.GetString(1),
                            Model = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            CreatedAt = reader.GetDateTime(4),
                            LoadCapacity = reader.GetDecimal(5),
                            AxleCount = reader.GetInt32(6)
                        });
                    }
                }
            }
            return trucks;
        }
        public void UpdateTruck(Truck truck)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "UPDATE trucks SET brand = @brand, model = @model, year = @year, " +
                    "load_capacity = @loadCapacity, axle_count = @axleCount " +
                    "WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", truck.Id);
                    cmd.Parameters.AddWithValue("@brand", truck.Brand);
                    cmd.Parameters.AddWithValue("@model", truck.Model);
                    cmd.Parameters.AddWithValue("@year", truck.Year);
                    cmd.Parameters.AddWithValue("@loadCapacity", truck.LoadCapacity);
                    cmd.Parameters.AddWithValue("@axleCount", truck.AxleCount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTruck(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM trucks WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // CRUD операции для Motorcycle (аналогично Car)
        public void AddMotorcycle(Motorcycle motorcycle)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "INSERT INTO motorcycles (brand, model, year, engine_volume, is_racing) " +
                    "VALUES (@brand, @model, @year, @engineVolume, @isRacing)", conn))
                {
                    cmd.Parameters.AddWithValue("@brand", motorcycle.Brand);
                    cmd.Parameters.AddWithValue("@model", motorcycle.Model);
                    cmd.Parameters.AddWithValue("@year", motorcycle.Year);
                    cmd.Parameters.AddWithValue("@engineVolume", motorcycle.EngineVolume);
                    cmd.Parameters.AddWithValue("@isRacing", motorcycle.IsRacing);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Motorcycle> GetAllMotorcycles()
        {
            var motorcycles = new List<Motorcycle>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM motorcycles", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        motorcycles.Add(new Motorcycle
                        {
                            Id = reader.GetInt32(0),
                            Brand = reader.GetString(1),
                            Model = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            CreatedAt = reader.GetDateTime(4),
                            EngineVolume = reader.GetDecimal(5),
                            IsRacing = reader.GetBoolean(6)
                        });
                    }
                }
            }
            return motorcycles;
        }
        public void UpdateMotorcycle(Motorcycle motorcycle)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "UPDATE motorcycles SET brand = @brand, model = @model, year = @year, " +
                    "engine_volume = @engineVolume, is_racing = @isRacing " +
                    "WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", motorcycle.Id);
                    cmd.Parameters.AddWithValue("@brand", motorcycle.Brand);
                    cmd.Parameters.AddWithValue("@model", motorcycle.Model);
                    cmd.Parameters.AddWithValue("@year", motorcycle.Year);
                    cmd.Parameters.AddWithValue("@engineVolume", motorcycle.EngineVolume);
                    cmd.Parameters.AddWithValue("@isRacing", motorcycle.IsRacing);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMotorcycle(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM motorcycles WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Получение всех транспортных средств (из всех таблиц)
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            var vehicles = new List<Vehicle>();

            // Получаем все автомобили
            vehicles.AddRange(GetAllCars());

            // Получаем все грузовики
            vehicles.AddRange(GetAllTrucks());

            // Получаем все мотоциклы
            vehicles.AddRange(GetAllMotorcycles());

            return vehicles;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _2_SRS_DB
{
    internal class VehicleDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public VehicleDbContext()
        { 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Username = postgres; Password = 5dartyr5; Database = Vehicle");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("vehicles");
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("cars");
                entity.HasBaseType<Vehicle>();
                entity.Property(c => c.PassengerCapacity).HasColumnName("passenger_capacity");
                entity.Property(c => c.BodyType).HasColumnName("body_type");
            });

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.ToTable("trucks");
                entity.HasBaseType<Vehicle>();
                entity.Property(t => t.LoadCapacity).HasColumnName("load_capacity").HasColumnType("decimal(10,2)");
                entity.Property(t => t.AxleCount).HasColumnName("axle_count");
            });

            modelBuilder.Entity<Motorcycle>(entity =>
            {
                entity.ToTable("motorcycles");
                entity.HasBaseType<Vehicle>();
                entity.Property(m => m.EngineVolume).HasColumnName("engine_volume").HasColumnType("decimal(5,2)");
                entity.Property(m => m.IsRacing).HasColumnName("is_racing");
            });
        }
    }
}

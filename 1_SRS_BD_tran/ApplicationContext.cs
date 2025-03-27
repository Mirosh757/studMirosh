using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _1_lab_BD_tran
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<users> users => Set<users>();
        public DbSet<accounts> accounts => Set<accounts>();
        public DbSet<tags> tags => Set<tags>();
        public ApplicationContext()
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Username = postgres; Password = 5dartyr5; Database = UsersAndTags");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accounts>(entity =>
            {
                entity.HasKey(e => e.user_id).HasName("accounts_pkey");

                entity.ToTable("accounts");

                entity.Property(e => e.user_id)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");

                entity.Property(e => e.tags)
                .HasDefaultValue(new int[1] { -1});

                entity.HasOne(d => d.Users).WithOne(p => p.Accounts)
                    .HasForeignKey<accounts>(d => d.user_id)
                    .HasConstraintName("user_id_foreign");
            });
        }
    }
}

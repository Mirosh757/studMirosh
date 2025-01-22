using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _5_lab_No_Pattern
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Region> regions => Set<Region>();
        public ApplicationContext() 
        { 
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Username = postgres; Password = 5dartyr5; Database = Region");
        }
    }
}

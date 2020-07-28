using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Database
{
    public class DAL : DbContext
    {
        private const string connectionString = "Server=localhost;Database=STRPG-DEV;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Entities"));
        }

        public DbSet<User> Users { get; set; }
    }
}

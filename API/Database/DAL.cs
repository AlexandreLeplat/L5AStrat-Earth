using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class DAL : DbContext
    {
        private const string connectionString = "Server=localhost;Database=STRPG-DEV;Trusted_Connection=True;";

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("API"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User() { Id = 1, Name = "Admin", Password = "$2a$11$0cCo4Ciq8/0QZszDtAkP.eK969i/yEeK0bLIU3Tr8Zrut/BxkT5wS", Role = UserRole.None }
                                              , new User() { Id = 2, Name = "Dragon", Password = "$2a$11$1lokWFVxSB.CnBrMpKlFNOnKJm5w04ZCgEX4SBRyz83OYXU7XOqla", Role = UserRole.None }
                                              , new User() { Id = 3, Name = "Grue", Password = "$2a$11$A8cdb6KPnCYxTdEz42jzZ.UOCNlF/9jMg/KuVf6Dm0DdoFUU2N1Bu", Role = UserRole.None }
                                              , new User() { Id = 4, Name = "Lion", Password = "$2a$11$i3YhcWnD0DFn9mq/geDUD.XBqvWGqb1kn/7nJtPxFeX3vfxc3w8Ie", Role = UserRole.None }
                                              , new User() { Id = 5, Name = "Scorpion", Password = "$2a$11$.B24rVXy6wHIdKC1uF49dOzem5wFa4nFbD3PumlzAaXDnmhP67T6O", Role = UserRole.None });
        }
    }
}

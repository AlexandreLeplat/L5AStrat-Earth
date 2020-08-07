using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace API.Database
{
    public class DAL : DbContext
    {
        private const string connectionString = "Server=localhost;Database=STRPG-DEV;Trusted_Connection=True;";

        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("API"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(o => o._jsonHomeWidgets).HasColumnName("HomeWidgets");
            modelBuilder.Entity<Game>().HasData(new Game() { Id = 1, Name = "L5A Strat - TERRE", HomeWidgets = new List<string>() { "Clock", "PlayerInfo", "CampaignInfo" } });

            modelBuilder.Entity<Campaign>().Property(o => o._jsonAssets).HasColumnName("Assets");
            modelBuilder.Entity<Campaign>().HasData(new Campaign() { Id = 1, Name = "La bataille des Quatre Vents", CurrentTurn = 3, GameId = 1, NextPhase = DateTime.Now.AddDays(1), PhaseLength = 24,
                                            CurrentPhase = TurnPhase.Early, Assets = new Dictionary<string, Dictionary<string, string>>()
                                                {
                                                    { "Classement", new Dictionary<string, string> () {{"1er", "Doji Misao"}, { "2ème", "Kitsuki Hisao" }, { "3ème", "Yogo Rushi" }, { "4ème", "Ikoma Kiyoshi" } } },
                                                    { "Prochain classement", new Dictionary<string, string> () {{"Tour 6", "" } } }
                                                }});

            modelBuilder.Entity<User>().HasData(new User() { Id = 1, Name = "Admin", Password = "$2a$11$0cCo4Ciq8/0QZszDtAkP.eK969i/yEeK0bLIU3Tr8Zrut/BxkT5wS", Role = UserRole.Admin }
                                              , new User() { Id = 2, Name = "Dragon", Password = "$2a$11$1lokWFVxSB.CnBrMpKlFNOnKJm5w04ZCgEX4SBRyz83OYXU7XOqla", Role = UserRole.None }
                                              , new User() { Id = 3, Name = "Grue", Password = "$2a$11$A8cdb6KPnCYxTdEz42jzZ.UOCNlF/9jMg/KuVf6Dm0DdoFUU2N1Bu", Role = UserRole.None }
                                              , new User() { Id = 4, Name = "Lion", Password = "$2a$11$i3YhcWnD0DFn9mq/geDUD.XBqvWGqb1kn/7nJtPxFeX3vfxc3w8Ie", Role = UserRole.None }
                                              , new User() { Id = 5, Name = "Scorpion", Password = "$2a$11$.B24rVXy6wHIdKC1uF49dOzem5wFa4nFbD3PumlzAaXDnmhP67T6O", Role = UserRole.None });

            modelBuilder.Entity<Player>().Property(o => o._jsonAssets).HasColumnName("Assets");
            modelBuilder.Entity<Player>().HasData(new Player() { Id = 1, Name = "Admin", UserId = 1, CampaignId = 1, IsCurrentPlayer = true }
                                                , new Player() { Id = 2, Name = "Kitsuki Hisao", UserId = 2, CampaignId = 1, IsCurrentPlayer = true, Color = "green",
                                                    Assets = new Dictionary<string, Dictionary<string, string>>()
                                                {
                                                    { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "5"}, { "Infamie", "1" } } },
                                                    { "Ressources", new Dictionary<string, string> () {{"Stratégie", "3"}, { "Influence", "0" } } }
                                                }}
                                                , new Player() { Id = 3, Name = "Doji Misao", UserId = 3, CampaignId = 1, IsCurrentPlayer = true, Color = "cyan",
                                                    Assets = new Dictionary<string, Dictionary<string, string>>()
                                                {
                                                    { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "7"}, { "Infamie", "0" } } },
                                                    { "Ressources", new Dictionary<string, string> () {{"Stratégie", "2"}, { "Influence", "1" } } }
                                                }}
                                                , new Player() { Id = 4, Name = "Ikoma Kiyoshi", UserId = 4, CampaignId = 1, IsCurrentPlayer = true, Color = "yellow",
                                                    Assets = new Dictionary<string, Dictionary<string, string>>()
                                                {
                                                    { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "3"}, { "Infamie", "0" } } },
                                                    { "Ressources", new Dictionary<string, string> () {{"Stratégie", "5"}, { "Influence", "0" } } }
                                                }}
                                                , new Player() { Id = 5, Name = "Yogo Rushi", UserId = 5, CampaignId = 1, IsCurrentPlayer = true, Color = "red",
                                                    Assets = new Dictionary<string, Dictionary<string, string>>()
                                                {
                                                    { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "4"}, { "Infamie", "2" } } },
                                                    { "Ressources", new Dictionary<string, string> () {{"Stratégie", "4"}, { "Influence", "0" } } }
                                                }});
        }
    }
}

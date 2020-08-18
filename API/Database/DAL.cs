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

        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrdersSheet> OrdersSheets { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("API"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(o => o._jsonHomeWidgets).HasColumnName("HomeWidgets");
            modelBuilder.Entity<Game>().HasData(new Game() { Id = 1, Name = "L5A Strat - TERRE", HomeWidgets = new List<string>() { "Clock", "PlayerInfo", "CampaignInfo" } });

            modelBuilder.Entity<Campaign>().Property(o => o._jsonAssets).HasColumnName("Assets");
            modelBuilder.Entity<Campaign>().HasData(new Campaign()
            {
                Id = 1,
                Name = "La bataille des Quatre Vents",
                CurrentTurn = 3,
                GameId = 1,
                NextPhase = DateTime.Now.AddDays(1),
                PhaseLength = 24,
                CurrentPhase = TurnPhase.Early,
                Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Classement", new Dictionary<string, string> () {{"1er", "Doji Misao"}, { "2ème", "Kitsuki Hisao" }, { "3ème", "Yogo Rushi" }, { "4ème", "Ikoma Kiyoshi" } } },
                        { "Prochain classement", new Dictionary<string, string> () {{"Tour 6", "" } } }
                    }
            });

            modelBuilder.Entity<User>().HasData(new User() { Id = 1, Name = "Admin", Password = "$2a$11$0cCo4Ciq8/0QZszDtAkP.eK969i/yEeK0bLIU3Tr8Zrut/BxkT5wS", Role = UserRole.Admin }
                                              , new User() { Id = 2, Name = "Dragon", Password = "$2a$11$1lokWFVxSB.CnBrMpKlFNOnKJm5w04ZCgEX4SBRyz83OYXU7XOqla", Role = UserRole.None }
                                              , new User() { Id = 3, Name = "Grue", Password = "$2a$11$A8cdb6KPnCYxTdEz42jzZ.UOCNlF/9jMg/KuVf6Dm0DdoFUU2N1Bu", Role = UserRole.None }
                                              , new User() { Id = 4, Name = "Lion", Password = "$2a$11$i3YhcWnD0DFn9mq/geDUD.XBqvWGqb1kn/7nJtPxFeX3vfxc3w8Ie", Role = UserRole.None }
                                              , new User() { Id = 5, Name = "Scorpion", Password = "$2a$11$.B24rVXy6wHIdKC1uF49dOzem5wFa4nFbD3PumlzAaXDnmhP67T6O", Role = UserRole.None });

            modelBuilder.Entity<Player>().Property(o => o._jsonAssets).HasColumnName("Assets");
            modelBuilder.Entity<Player>().HasData(new Player() { Id = 1, Name = "Admin", UserId = 1, CampaignId = 1, IsCurrentPlayer = true },
                new Player()
                {
                    Id = 2,
                    Name = "Kitsuki Hisao",
                    UserId = 2,
                    CampaignId = 1,
                    IsCurrentPlayer = true,
                    Color = "green",
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "5"}, { "Infamie", "1" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "3"}, { "Influence", "0" } } }
                    }
                },
                new Player()
                {
                    Id = 3,
                    Name = "Doji Misao",
                    UserId = 3,
                    CampaignId = 1,
                    IsCurrentPlayer = true,
                    Color = "cyan",
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "7"}, { "Infamie", "0" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "2"}, { "Influence", "1" } } }
                    }
                }
                , new Player()
                {
                    Id = 4,
                    Name = "Ikoma Kiyoshi",
                    UserId = 4,
                    CampaignId = 1,
                    IsCurrentPlayer = true,
                    Color = "yellow",
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "3"}, { "Infamie", "0" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "5"}, { "Influence", "0" } } }
                    }
                }
                , new Player()
                {
                    Id = 5,
                    Name = "Yogo Rushi",
                    UserId = 5,
                    CampaignId = 1,
                    IsCurrentPlayer = true,
                    Color = "red",
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "4"}, { "Infamie", "2" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "4"}, { "Influence", "0" } } }
                    }
                });

            modelBuilder.Entity<ActionType>().Property(o => o._jsonForm).HasColumnName("Form");
            modelBuilder.Entity<ActionType>().HasData(new ActionType()
            {
                Id = 1,
                Label = "Planification",
                Description = "Gagnez un point de Stratégie",
                GameId = 1,
                Form = new List<OrderInput>() {
                    { new OrderInput() {
                        Label = "Augmentation", Type = OrderInputType.Checkbox,
                        Description = "Manoeuvre sournoise : Gagnez 2 points d'Infamie pour gagner un point de Stratégie supplémentaire" } }
                    }
            },
                new ActionType()
                {
                    Id = 2,
                    Label = "Flatterie",
                    Description = "Gagnez 2 points de Gloire et faites-en gagner 2 à un adversaire",
                    GameId = 1,
                    Form = new List<OrderInput>() {
                    { new OrderInput() {
                        Label = "Cible", Type = OrderInputType.Opponent,
                        Description = "L'adversaire recevant 2 points de Gloire" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = OrderInputType.Checkbox,
                        Description = "Faveur politique : Dépensez un point d’Influence pour gagner 3 points de Gloire supplémentaires et ne cibler personne" } }
                    }
                },
                new ActionType()
                {
                    Id = 3,
                    Label = "Médisance",
                    Description = "Infligez 3 points d’Infamie à un adversaire et gagnez-en vous-même un point",
                    GameId = 1,
                    Form = new List<OrderInput>() {
                    { new OrderInput() {
                        Label = "Cible", Type = OrderInputType.Opponent,
                        Description = "L'adversaire subissant la médisance" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = OrderInputType.Checkbox,
                        Description = "Scandale : Dépensez un point d’Influence pour ajouter 2 points d’Infamie à votre cible et remplacer votre gain d'Infamie par un gain de Gloire." } }
                    }
                },
                new ActionType()
                {
                    Id = 4,
                    Label = "Renforts",
                    Description = "Déployez une armée pour 5 points de Stratégie",
                    GameId = 1,
                    Form = new List<OrderInput>() {
                    { new OrderInput() {
                        Label = "Case", Type = OrderInputType.EntryTile,
                        Description = "La case de renfort ciblée" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = OrderInputType.Checkbox,
                        Description = "Soutien militaire : Dépensez un point d'Influence à la place des points de Stratégie." } }
                    }
                },
                new ActionType()
                {
                    Id = 5,
                    Label = "Déplacement",
                    Description = "Déplacez une armée d'une case",
                    GameId = 1,
                    Form = new List<OrderInput>() {
                    { new OrderInput() {
                        Label = "Armée", Type = OrderInputType.Unit,
                        Description = "L'armée à déplacer" } },
                    { new OrderInput() {
                        Label = "Destination", Type = OrderInputType.UnitMove,
                        Description = "La destination de l'armée" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = OrderInputType.Checkbox,
                        Description = "Ligne de ravitaillement : Dépensez un point d'Influence pour déplacer l'armée après un renfort ou un changement de formation." } }
                    }
                },
                new ActionType()
                {
                    Id = 6,
                    Label = "Formation",
                    Description = "Changez la formation de l'armée",
                    GameId = 1,
                    Form = new List<OrderInput>() {
                    { new OrderInput() {
                        Label = "Armée", Type = OrderInputType.Unit,
                        Description = "L'armée ciblée" } },
                    { new OrderInput() {
                        Label = "Formation", Type = OrderInputType.Formation,
                        Description = "La formation à adopter" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = OrderInputType.Checkbox,
                        Description = "Discipline : Dépensez un point de Stratégie pour changer de formation après un déplacement." } }
                    }
                },
                new ActionType()
                {
                    Id = 7,
                    Label = "Renseignements",
                    Description = "Subissez un point d'Infamie pour espionner un adversaire",
                    GameId = 1,
                    Form = new List<OrderInput>() {
                    { new OrderInput() {
                        Label = "Cible", Type = OrderInputType.Opponent,
                        Description = "L'adversaire à espionner" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = OrderInputType.Checkbox,
                        Description = "Réseau d'information : Dépensez un point d'Influence pour annuler l'Infamie et cibler tous les adversaires." } }
                    }
                });

            modelBuilder.Entity<OrdersSheet>().HasData(
                new OrdersSheet() { Id = 1, PlayerId = 2, Turn = 3, Priority = 0, MaxOrdersCount = 5 },
                new OrdersSheet() { Id = 2, PlayerId = 3, Turn = 3, Priority = 0, MaxOrdersCount = 5 },
                new OrdersSheet() { Id = 3, PlayerId = 4, Turn = 3, Priority = 0, MaxOrdersCount = 5 },
                new OrdersSheet() { Id = 4, PlayerId = 5, Turn = 3, Priority = 0, MaxOrdersCount = 5 });

            modelBuilder.Entity<Order>().Property(o => o._jsonParameters).HasColumnName("Parameters");
            modelBuilder.Entity<Order>().HasOne(e => e.ActionType).WithMany(e => e.Orders).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    Id = 1, OrdersSheetId = 1, ActionTypeId = 1, Status = OrderStatus.Valid,
                    Parameters = new Dictionary<string, string>() { { "Augmentation", "false" } },
                    Comment = "+ 1 Strat"
                },
                new Order()
                {
                    Id = 2, OrdersSheetId = 2, ActionTypeId = 2, Status = OrderStatus.Valid,
                    Parameters = new Dictionary<string, string>() { { "Cible", "5" }, { "Augmentation", "false" } },
                    Comment = "+ 2 Gloire, Yogo Rushi + 2 Gloire"
                },
                new Order()
                {
                    Id = 3, OrdersSheetId = 3, ActionTypeId = 4, Status = OrderStatus.Valid,
                    Parameters = new Dictionary<string, string>() { { "Cible", "3" }, { "Augmentation", "false" } },
                    Comment = "+ 1 Infamie, Doji Misao +3 Infamie"
                },
                new Order()
                {
                    Id = 4, OrdersSheetId = 4, ActionTypeId = 1, Status = OrderStatus.Valid,
                    Parameters = new Dictionary<string, string>() { { "Augmentation", "false" } },
                    Comment = "+ 1 Strat"
                });
        }
    }
}

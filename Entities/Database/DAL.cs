using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Entities.Database
{
    public class DAL : DbContext
    {
        public DAL(DbContextOptions<DAL> options)
            : base(options)
        { }

        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<MapTile> MapTiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrdersSheet> OrdersSheets { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }

        private long _unitId = 1;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(o => o._jsonHomeWidgets).HasColumnName("HomeWidgets");
            modelBuilder.Entity<Game>().HasData(new Game() { Id = 1, Name = "L5A Strat - TERRE", HomeWidgets = new List<string>() { "Clock", "PlayerInfo", "CampaignInfo" } });

            modelBuilder.Entity<Campaign>().Property(o => o._jsonAssets).HasColumnName("Assets");
            modelBuilder.Entity<Campaign>().HasData(new Campaign()
            {
                Id = 1,
                Name = "La bataille des Quatre Vents",
                CurrentTurn = 1,
                GameId = 1,
                NextPhase = DateTime.Today.AddDays(1).AddHours(6),
                PhaseLength = 720,
                CurrentPhase = TurnPhase.Early,
                Status = CampaignStatus.Running,
                Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Prochain classement", new Dictionary<string, string> () {{"Tour 3", "" } } }
                    }
            }); ;

            modelBuilder.Entity<User>().HasData(new User() { Id = 1, Name = "Admin", Password = "$2a$11$BjZvEi2T4jXl.dONfcedm.Ll0sL6d226qBIxR.PT0G1LwLk6jJmQO", Role = UserRole.Admin }
                                              , new User() { Id = 2, Name = "toutétékalculé", Password = "$2a$11$yePEmRHE5RMu0lOXIxzt9.Z9sIda516qP3uICCmR2OVizTTDExVXi", Role = UserRole.None }
                                              , new User() { Id = 3, Name = "Elostirion ", Password = "$2a$11$9nUwVLN8btVzPvLZAnUWDeFjMaUxzmfZH6TXjLFsPRMhXeeryXUOu", Role = UserRole.None }
                                              , new User() { Id = 4, Name = "Akodostef", Password = "$2a$11$jA8Nys/YYvA7EKVqNTgYzeUREtgvklcFtrY2kBEoD7VdG9IUAayyO", Role = UserRole.None }
                                              , new User() { Id = 5, Name = "Crabi", Password = "$2a$11$H9XVGihkqHDssIMjGZW9NefJoocnv2y6NJ4PwVEIEosyp2QnjgQMy", Role = UserRole.None });

            modelBuilder.Entity<Player>().Property(o => o._jsonAssets).HasColumnName("Assets");
            modelBuilder.Entity<Player>().HasData(new Player() { Id = 1, Name = "Admin", UserId = 1, CampaignId = 1, IsCurrentPlayer = true, IsAdmin = true },
                new Player()
                {
                    Id = 2,
                    Name = "Matsu Kiperuganyu",
                    UserId = 2,
                    CampaignId = 1,
                    IsCurrentPlayer = true,
                    Color = "gold",
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "5"}, { "Infamie", "0" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "5"}, { "Influence", "0" } } }
                    }
                },
                new Player()
                {
                    Id = 3,
                    Name = "Doji Ujitsu",
                    UserId = 3,
                    CampaignId = 1,
                    IsCurrentPlayer = true,
                    Color = "cyan",
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "5"}, { "Infamie", "0" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "5"}, { "Influence", "0" } } }
                    }
                }
                , new Player()
                {
                    Id = 4,
                    Name = "Akodo Yama",
                    UserId = 4,
                    CampaignId = 1,
                    IsCurrentPlayer = true,
                    Color = "yellow",
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "5"}, { "Infamie", "0" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "5"}, { "Influence", "0" } } }
                    }
                }
                , new Player()
                {
                    Id = 5,
                    Name = "Togashi Atsu",
                    UserId = 5,
                    CampaignId = 1,
                    IsCurrentPlayer = true,
                    Color = "green",
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "5"}, { "Infamie", "0" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "5"}, { "Influence", "0" } } }
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
                            Label = "Augmentation", Type = "Checkbox",
                            Description = "Manoeuvre sournoise : Gagnez 2 points d'Infamie pour gagner un point de Stratégie supplémentaire" } }
                        }
                },
                new ActionType()
                {
                    Id = 2,
                    Label = "Flatterie",
                    Description = "Gagnez 1 point de Gloire et faites-en gagner 2 à un adversaire",
                    GameId = 1,
                    Form = new List<OrderInput>() {
                    { new OrderInput() {
                        Label = "Cible", Type = "Opponent",
                        Description = "L'adversaire recevant 2 points de Gloire" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = "Checkbox",
                        Description = "Faveur politique : Dépensez un point d’Influence pour gagner 4 points de Gloire supplémentaires et ne cibler personne" } }
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
                        Label = "Cible", Type = "Opponent",
                        Description = "L'adversaire subissant la médisance" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = "Checkbox",
                        Description = "Scandale : Dépensez un point d’Influence pour ajouter 3 points d’Infamie à votre cible et remplacer votre gain d'Infamie par un gain de Gloire." } }
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
                        Label = "Case", Type = "EntryTile",
                        Description = "La case de renfort ciblée" } },
                    { new OrderInput() {
                        Label = "Formation", Type = "Formation",
                        Description = "La formation à adopter" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = "Checkbox",
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
                        Label = "Armée", Type = "Army",
                        Description = "L'armée à déplacer" } },
                    { new OrderInput() {
                        Label = "Destination", Type = "Move", Parameter = "Armée",
                        Description = "La destination de l'armée" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = "Checkbox",
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
                        Label = "Armée", Type = "Army",
                        Description = "L'armée ciblée" } },
                    { new OrderInput() {
                        Label = "Formation", Type = "Formation", Parameter = "Armée",
                        Description = "La formation à adopter" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = "Checkbox",
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
                        Label = "Cible", Type = "Opponent",
                        Description = "L'adversaire à espionner" } },
                    { new OrderInput() {
                        Label = "Augmentation", Type = "Checkbox",
                        Description = "Réseau d'information : Dépensez un point d'Influence pour annuler l'Infamie et cibler tous les adversaires." } }
                    }
                });

            modelBuilder.Entity<OrdersSheet>().HasData(
                new OrdersSheet() { Id = 1, PlayerId = 2, Turn = 3, Priority = 0, MaxOrdersCount = 5, Status = OrdersSheetStatus.Writing },
                new OrdersSheet() { Id = 2, PlayerId = 3, Turn = 3, Priority = 0, MaxOrdersCount = 5, Status = OrdersSheetStatus.Writing },
                new OrdersSheet() { Id = 3, PlayerId = 4, Turn = 3, Priority = 0, MaxOrdersCount = 5, Status = OrdersSheetStatus.Writing },
                new OrdersSheet() { Id = 4, PlayerId = 5, Turn = 3, Priority = 0, MaxOrdersCount = 5, Status = OrdersSheetStatus.Writing });

            modelBuilder.Entity<Order>().Property(o => o._jsonParameters).HasColumnName("Parameters");
            modelBuilder.Entity<Order>().HasOne(e => e.ActionType).WithMany(e => e.Orders).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Map>().HasOne(m => m.Player).WithMany(p => p.Maps).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Map>().HasData(new Map()
                {
                    Id = 1,
                    CampaignId = 1,
                    CreationDate = DateTime.Now,
                    Name = "Début de Tour 1",
                    Turn = 1,
                    PlayerId = 1
                });

            modelBuilder.Entity<MapTile>().Property(o => o._jsonAssets).HasColumnName("Assets");
            modelBuilder.Entity<MapTile>().HasData(this.GenerateMap(1));

            modelBuilder.Entity<Unit>().HasOne(m => m.Player).WithMany(p => p.Units).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Unit>().Property(o => o._jsonAssets).HasColumnName("Assets");
            modelBuilder.Entity<Unit>().HasData(new List<Unit>()
            {
                CreateBuilding("Forteresse", 4, 4, 1),
                CreateBuilding("Avant-Poste", 1, 1, 1),
                CreateBuilding("Avant-Poste", 6, 6, 1),
                CreateBuilding("Village", 2, 3, 1),
                CreateBuilding("Village", 3, 6, 1),
                CreateBuilding("Village", 5, 2, 1),
                CreateBuilding("Village", 6, 5, 1),
                CreateBuilding("Entrée", 0, 3, 2),
                CreateBuilding("Entrée", 0, 4, 2),
                CreateBuilding("Entrée", 0, 5, 2),
                CreateBuilding("Entrée", 0, 6, 2),
                CreateBuilding("Entrée", 3, 0, 3),
                CreateBuilding("Entrée", 4, 0, 3),
                CreateBuilding("Entrée", 5, 0, 3),
                CreateBuilding("Entrée", 6, 0, 3),
                CreateBuilding("Entrée", 2, 8, 4),
                CreateBuilding("Entrée", 3, 8, 4),
                CreateBuilding("Entrée", 4, 8, 4),
                CreateBuilding("Entrée", 5, 8, 4),
                CreateBuilding("Entrée", 8, 2, 5),
                CreateBuilding("Entrée", 8, 3, 5),
                CreateBuilding("Entrée", 8, 4, 5),
                CreateBuilding("Entrée", 8, 5, 5)
            });

            modelBuilder.Entity<Message>().HasOne(m => m.Sender).WithMany(p => p.Messages).OnDelete(DeleteBehavior.NoAction);
        }

        private List<MapTile> GenerateMap(long mapId)
        {
            var result = new List<MapTile>();
            string coordinatesLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            long idTile = 1;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var tile = new MapTile()
                    {
                        Id = idTile,
                        X = i,
                        Y = j,
                        Color = ((i + j) % 2 == 0) ? "wheat" : "linen",
                        Name = "Plaine",
                        MapId = mapId,
                        Symbol = string.Empty,
                        Assets = new Dictionary<string, Dictionary<string, string>>()
                    };
                    
                    if (i == 0 && j >= 3 && j <= 6)
                    {
                        tile.Name = "Entrée";
                        tile.Symbol = "home";
                        tile.Assets = new Dictionary<string, Dictionary<string, string>>()
                        {
                            { "Propriétaire", new Dictionary<string, string> () { { "Kitsuki Hisao", "" } } }
                        };
                    }
                    if (i == 8 && j >= 2 && j <= 5)
                    {
                        tile.Name = "Entrée";
                        tile.Symbol = "home";
                        tile.Assets = new Dictionary<string, Dictionary<string, string>>()
                        {
                            { "Propriétaire", new Dictionary<string, string> () { { "Doji Misao", "" } } }
                        };
                    }
                    if (j == 0 && i >= 3 && i <= 6)
                    {
                        tile.Name = "Entrée";
                        tile.Symbol = "home";
                        tile.Assets = new Dictionary<string, Dictionary<string, string>>()
                        {
                            { "Propriétaire", new Dictionary<string, string> () { { "Ikoma Kiyoshi", "" } } }
                        };
                    }
                    if (j == 8 && i >= 2 && i <= 5)
                    {
                        tile.Name = "Entrée";
                        tile.Symbol = "home";
                        tile.Assets = new Dictionary<string, Dictionary<string, string>>()
                        {
                            { "Propriétaire", new Dictionary<string, string> () { { "Yogo Rushi", "" } } }
                        };
                    }

                    if (i == 2 && j == 3
                        || i == 5 && j == 2
                        || i == 6 && j == 5
                        || i == 3 && j == 6)
                    {
                        tile.Name = "Village";
                        tile.Symbol = "house";
                        tile.Assets = new Dictionary<string, Dictionary<string, string>>()
                        {
                            { "Production", new Dictionary<string, string> () { { "Gloire", "2" } } }
                        };
                    }

                    if (i == 1 && j == 1
                        || i == 7 && j == 7)
                    {
                        tile.Name = "Avant-poste";
                        tile.Symbol = "tower";
                        tile.Assets = new Dictionary<string, Dictionary<string, string>>()
                        {
                            { "Production", new Dictionary<string, string> () { { "Gloire", "1" }, { "Stratégie", "2" } } }
                        };
                    }

                    if (i == 4 && j == 4)
                    {
                        tile.Name = "Forteresse";
                        tile.Symbol = "castle";
                        var prod = new Dictionary<string, string>();
                        tile.Assets = new Dictionary<string, Dictionary<string, string>>()
                        {
                            { "Production", new Dictionary<string, string> () { { "Gloire", "3" }, { "Stratégie", "2" } } }
                        };
                    }
                    tile.Name += $" ({coordinatesLetters[i]}{j+1})";
                    result.Add(tile);
                    idTile++;
                }
            }
            return result;
        }

        private Unit CreateBuilding(string type, int x, int y, long playerId)
        {
            var newUnit = new Unit()
            {
                Id = _unitId,
                Name = type,
                Type = "Building",
                PlayerId = playerId,
                X = x,
                Y = y,
                Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Type", new Dictionary<string, string> () { { type, null } } }
                    }
            };
            _unitId++;
            return newUnit;
        }
    }
}

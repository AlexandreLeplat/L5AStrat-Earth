﻿// <auto-generated />
using System;
using Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HostApp.Migrations
{
    [DbContext(typeof(DAL))]
    partial class DALModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.ActionType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_jsonForm")
                        .HasColumnName("Form")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("ActionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "Gagnez un point de Stratégie",
                            GameId = 1L,
                            Label = "Planification",
                            _jsonForm = "[{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Manoeuvre sournoise : Gagnez 2 points d\\u0027Infamie pour gagner un point de Strat\\u00E9gie suppl\\u00E9mentaire\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "Gagnez 1 point de Gloire et faites-en gagner 2 à un adversaire",
                            GameId = 1L,
                            Label = "Flatterie",
                            _jsonForm = "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 4 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Infligez 3 points d’Infamie à un adversaire et gagnez-en vous-même un point",
                            GameId = 1L,
                            Label = "Médisance",
                            _jsonForm = "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 3 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]"
                        },
                        new
                        {
                            Id = 4L,
                            Description = "Déployez une armée pour 5 points de Stratégie",
                            GameId = 1L,
                            Label = "Renforts",
                            _jsonForm = "[{\"Label\":\"Case\",\"Type\":\"EntryTile\",\"Description\":\"La case de renfort cibl\\u00E9e\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":true,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Formation\",\"Type\":\"Formation\",\"Description\":\"La formation \\u00E0 adopter\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Soutien militaire : D\\u00E9pensez un point d\\u0027Influence \\u00E0 la place des points de Strat\\u00E9gie.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]"
                        },
                        new
                        {
                            Id = 5L,
                            Description = "Déplacez une armée d'une case",
                            GameId = 1L,
                            Label = "Déplacement",
                            _jsonForm = "[{\"Label\":\"Arm\\u00E9e\",\"Type\":\"Army\",\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\",\"Parameter\":null,\"IsPredefinedOnMap\":true,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Destination\",\"Type\":\"Move\",\"Description\":\"La destination de l\\u0027arm\\u00E9e\",\"Parameter\":\"Arm\\u00E9e\",\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":true,\"MapDescription\":\"S\\u00E9lectionnez la destination sur la carte\"},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Ligne de ravitaillement : D\\u00E9pensez un point d\\u0027Influence pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort ou un changement de formation.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]"
                        },
                        new
                        {
                            Id = 6L,
                            Description = "Changez la formation de l'armée",
                            GameId = 1L,
                            Label = "Formation",
                            _jsonForm = "[{\"Label\":\"Arm\\u00E9e\",\"Type\":\"Army\",\"Description\":\"L\\u0027arm\\u00E9e cibl\\u00E9e\",\"Parameter\":null,\"IsPredefinedOnMap\":true,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Formation\",\"Type\":\"Formation\",\"Description\":\"La formation \\u00E0 adopter\",\"Parameter\":\"Arm\\u00E9e\",\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Discipline : D\\u00E9pensez un point de Strat\\u00E9gie pour changer de formation apr\\u00E8s un d\\u00E9placement.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]"
                        },
                        new
                        {
                            Id = 7L,
                            Description = "Subissez un point d'Infamie pour espionner un adversaire",
                            GameId = 1L,
                            Label = "Renseignements",
                            _jsonForm = "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire \\u00E0 espionner\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"R\\u00E9seau d\\u0027information : D\\u00E9pensez un point d\\u0027Influence pour annuler l\\u0027Infamie et cibler tous les adversaires.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]"
                        });
                });

            modelBuilder.Entity("Entities.Models.Campaign", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentPhase")
                        .HasColumnType("int");

                    b.Property<int>("CurrentTurn")
                        .HasColumnType("int");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NextPhase")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhaseLength")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("_jsonAssets")
                        .HasColumnName("Assets")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Campaigns");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CurrentPhase = 1,
                            CurrentTurn = 1,
                            GameId = 1L,
                            Name = "La bataille des Quatre Vents",
                            NextPhase = new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999),
                            PhaseLength = 720,
                            Status = 2,
                            _jsonAssets = "{\"Prochain classement\":{\"Tour 3\":\"\"}}"
                        });
                });

            modelBuilder.Entity("Entities.Models.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_jsonHomeWidgets")
                        .HasColumnName("HomeWidgets")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "L5A Strat - TERRE",
                            _jsonHomeWidgets = "[\"Clock\",\"PlayerInfo\",\"CampaignInfo\"]"
                        });
                });

            modelBuilder.Entity("Entities.Models.Map", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CampaignId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("Turn")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("Entities.Models.MapTile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BorderColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MapId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.Property<string>("_jsonAssets")
                        .HasColumnName("Assets")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_jsonParameters")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("MapTiles");
                });

            modelBuilder.Entity("Entities.Models.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeletedForSender")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNotification")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PreviousMessageId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Turn")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Entities.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ActionTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OrdersSheetId")
                        .HasColumnType("bigint");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("_jsonParameters")
                        .HasColumnName("Parameters")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActionTypeId");

                    b.HasIndex("OrdersSheetId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Entities.Models.OrdersSheet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxOrdersCount")
                        .HasColumnType("int");

                    b.Property<int>("MaxPriority")
                        .HasColumnType("int");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Turn")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("OrdersSheets");
                });

            modelBuilder.Entity("Entities.Models.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CampaignId")
                        .HasColumnType("bigint");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasNewMap")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCurrentPlayer")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("_jsonAssets")
                        .HasColumnName("Assets")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("UserId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CampaignId = 1L,
                            HasNewMap = false,
                            IsAdmin = true,
                            IsCurrentPlayer = true,
                            Name = "Admin",
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            CampaignId = 1L,
                            Color = "gold",
                            HasNewMap = false,
                            IsAdmin = false,
                            IsCurrentPlayer = true,
                            Name = "Matsu Kiperuganyu",
                            UserId = 2L,
                            _jsonAssets = "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}"
                        },
                        new
                        {
                            Id = 3L,
                            CampaignId = 1L,
                            Color = "cyan",
                            HasNewMap = false,
                            IsAdmin = false,
                            IsCurrentPlayer = true,
                            Name = "Doji Ujitsu",
                            UserId = 3L,
                            _jsonAssets = "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}"
                        },
                        new
                        {
                            Id = 4L,
                            CampaignId = 1L,
                            Color = "yellow",
                            HasNewMap = false,
                            IsAdmin = false,
                            IsCurrentPlayer = true,
                            Name = "Akodo Yama",
                            UserId = 4L,
                            _jsonAssets = "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}"
                        },
                        new
                        {
                            Id = 5L,
                            CampaignId = 1L,
                            Color = "green",
                            HasNewMap = false,
                            IsAdmin = false,
                            IsCurrentPlayer = true,
                            Name = "Togashi Atsu",
                            UserId = 5L,
                            _jsonAssets = "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}"
                        });
                });

            modelBuilder.Entity("Entities.Models.Unit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.Property<string>("_jsonAssets")
                        .HasColumnName("Assets")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Admin",
                            Password = "$2a$11$BjZvEi2T4jXl.dONfcedm.Ll0sL6d226qBIxR.PT0G1LwLk6jJmQO",
                            Role = 1
                        },
                        new
                        {
                            Id = 2L,
                            Name = "toutétékalculé",
                            Password = "$2a$11$yePEmRHE5RMu0lOXIxzt9.Z9sIda516qP3uICCmR2OVizTTDExVXi",
                            Role = 0
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Elostirion ",
                            Password = "$2a$11$9nUwVLN8btVzPvLZAnUWDeFjMaUxzmfZH6TXjLFsPRMhXeeryXUOu",
                            Role = 0
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Akodostef",
                            Password = "$2a$11$jA8Nys/YYvA7EKVqNTgYzeUREtgvklcFtrY2kBEoD7VdG9IUAayyO",
                            Role = 0
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Crabi",
                            Password = "$2a$11$H9XVGihkqHDssIMjGZW9NefJoocnv2y6NJ4PwVEIEosyp2QnjgQMy",
                            Role = 0
                        });
                });

            modelBuilder.Entity("Entities.Models.ActionType", b =>
                {
                    b.HasOne("Entities.Models.Game", "Game")
                        .WithMany("ActionTypes")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.Campaign", b =>
                {
                    b.HasOne("Entities.Models.Game", "Game")
                        .WithMany("Campaigns")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.Map", b =>
                {
                    b.HasOne("Entities.Models.Campaign", "Campaign")
                        .WithMany("Maps")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Player", "Player")
                        .WithMany("Maps")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.MapTile", b =>
                {
                    b.HasOne("Entities.Models.Map", "Map")
                        .WithMany("MapTiles")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.Message", b =>
                {
                    b.HasOne("Entities.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Player", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.Order", b =>
                {
                    b.HasOne("Entities.Models.ActionType", "ActionType")
                        .WithMany("Orders")
                        .HasForeignKey("ActionTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Entities.Models.OrdersSheet", "OrdersSheet")
                        .WithMany("Orders")
                        .HasForeignKey("OrdersSheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.OrdersSheet", b =>
                {
                    b.HasOne("Entities.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.Player", b =>
                {
                    b.HasOne("Entities.Models.Campaign", "Campaign")
                        .WithMany("Players")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.User", "User")
                        .WithMany("Players")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.Unit", b =>
                {
                    b.HasOne("Entities.Models.Player", "Player")
                        .WithMany("Units")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

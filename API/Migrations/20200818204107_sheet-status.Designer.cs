﻿// <auto-generated />
using System;
using API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(DAL))]
    [Migration("20200818204107_sheet-status")]
    partial class sheetstatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
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
                            _jsonForm = "[{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Manoeuvre sournoise : Gagnez 2 points d\\u0027Infamie pour gagner un point de Strat\\u00E9gie suppl\\u00E9mentaire\"}]"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "Gagnez 2 points de Gloire et faites-en gagner 2 à un adversaire",
                            GameId = 1L,
                            Label = "Flatterie",
                            _jsonForm = "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 3 points de Gloire suppl\\u00E9mentaires et ne cibler personne\"}]"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Infligez 3 points d’Infamie à un adversaire et gagnez-en vous-même un point",
                            GameId = 1L,
                            Label = "Médisance",
                            _jsonForm = "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 2 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\"}]"
                        },
                        new
                        {
                            Id = 4L,
                            Description = "Déployez une armée pour 5 points de Stratégie",
                            GameId = 1L,
                            Label = "Renforts",
                            _jsonForm = "[{\"Label\":\"Case\",\"Type\":3,\"Description\":\"La case de renfort cibl\\u00E9e\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Soutien militaire : D\\u00E9pensez un point d\\u0027Influence \\u00E0 la place des points de Strat\\u00E9gie.\"}]"
                        },
                        new
                        {
                            Id = 5L,
                            Description = "Déplacez une armée d'une case",
                            GameId = 1L,
                            Label = "Déplacement",
                            _jsonForm = "[{\"Label\":\"Arm\\u00E9e\",\"Type\":4,\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\"},{\"Label\":\"Destination\",\"Type\":5,\"Description\":\"La destination de l\\u0027arm\\u00E9e\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Ligne de ravitaillement : D\\u00E9pensez un point d\\u0027Influence pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort ou un changement de formation.\"}]"
                        },
                        new
                        {
                            Id = 6L,
                            Description = "Changez la formation de l'armée",
                            GameId = 1L,
                            Label = "Formation",
                            _jsonForm = "[{\"Label\":\"Arm\\u00E9e\",\"Type\":4,\"Description\":\"L\\u0027arm\\u00E9e cibl\\u00E9e\"},{\"Label\":\"Formation\",\"Type\":6,\"Description\":\"La formation \\u00E0 adopter\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Discipline : D\\u00E9pensez un point de Strat\\u00E9gie pour changer de formation apr\\u00E8s un d\\u00E9placement.\"}]"
                        },
                        new
                        {
                            Id = 7L,
                            Description = "Subissez un point d'Infamie pour espionner un adversaire",
                            GameId = 1L,
                            Label = "Renseignements",
                            _jsonForm = "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire \\u00E0 espionner\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"R\\u00E9seau d\\u0027information : D\\u00E9pensez un point d\\u0027Influence pour annuler l\\u0027Infamie et cibler tous les adversaires.\"}]"
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
                            CurrentPhase = 2,
                            CurrentTurn = 3,
                            GameId = 1L,
                            Name = "La bataille des Quatre Vents",
                            NextPhase = new DateTime(2020, 8, 19, 22, 41, 6, 694, DateTimeKind.Local).AddTicks(7165),
                            PhaseLength = 24,
                            _jsonAssets = "{\"Classement\":{\"1er\":\"Doji Misao\",\"2\\u00E8me\":\"Kitsuki Hisao\",\"3\\u00E8me\":\"Yogo Rushi\",\"4\\u00E8me\":\"Ikoma Kiyoshi\"},\"Prochain classement\":{\"Tour 6\":\"\"}}"
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

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("_jsonParameters")
                        .HasColumnName("Parameters")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActionTypeId");

                    b.HasIndex("OrdersSheetId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ActionTypeId = 1L,
                            Comment = "+ 1 Strat",
                            OrdersSheetId = 1L,
                            Status = 2,
                            _jsonParameters = "{\"Augmentation\":\"false\"}"
                        },
                        new
                        {
                            Id = 2L,
                            ActionTypeId = 2L,
                            Comment = "+ 2 Gloire, Yogo Rushi + 2 Gloire",
                            OrdersSheetId = 2L,
                            Status = 2,
                            _jsonParameters = "{\"Cible\":\"5\",\"Augmentation\":\"false\"}"
                        },
                        new
                        {
                            Id = 3L,
                            ActionTypeId = 4L,
                            Comment = "+ 1 Infamie, Doji Misao +3 Infamie",
                            OrdersSheetId = 3L,
                            Status = 2,
                            _jsonParameters = "{\"Cible\":\"3\",\"Augmentation\":\"false\"}"
                        },
                        new
                        {
                            Id = 4L,
                            ActionTypeId = 1L,
                            Comment = "+ 1 Strat",
                            OrdersSheetId = 4L,
                            Status = 2,
                            _jsonParameters = "{\"Augmentation\":\"false\"}"
                        });
                });

            modelBuilder.Entity("Entities.Models.OrdersSheet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxOrdersCount")
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

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            MaxOrdersCount = 5,
                            PlayerId = 2L,
                            Priority = 0,
                            Status = 1,
                            Turn = 3
                        },
                        new
                        {
                            Id = 2L,
                            MaxOrdersCount = 5,
                            PlayerId = 3L,
                            Priority = 0,
                            Status = 1,
                            Turn = 3
                        },
                        new
                        {
                            Id = 3L,
                            MaxOrdersCount = 5,
                            PlayerId = 4L,
                            Priority = 0,
                            Status = 1,
                            Turn = 3
                        },
                        new
                        {
                            Id = 4L,
                            MaxOrdersCount = 5,
                            PlayerId = 5L,
                            Priority = 0,
                            Status = 1,
                            Turn = 3
                        });
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
                            IsCurrentPlayer = true,
                            Name = "Admin",
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            CampaignId = 1L,
                            Color = "green",
                            IsCurrentPlayer = true,
                            Name = "Kitsuki Hisao",
                            UserId = 2L,
                            _jsonAssets = "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Strat\\u00E9gie\":\"3\",\"Influence\":\"0\"}}"
                        },
                        new
                        {
                            Id = 3L,
                            CampaignId = 1L,
                            Color = "cyan",
                            IsCurrentPlayer = true,
                            Name = "Doji Misao",
                            UserId = 3L,
                            _jsonAssets = "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"7\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"2\",\"Influence\":\"1\"}}"
                        },
                        new
                        {
                            Id = 4L,
                            CampaignId = 1L,
                            Color = "yellow",
                            IsCurrentPlayer = true,
                            Name = "Ikoma Kiyoshi",
                            UserId = 4L,
                            _jsonAssets = "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"3\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}"
                        },
                        new
                        {
                            Id = 5L,
                            CampaignId = 1L,
                            Color = "red",
                            IsCurrentPlayer = true,
                            Name = "Yogo Rushi",
                            UserId = 5L,
                            _jsonAssets = "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"4\",\"Infamie\":\"2\"},\"Ressources\":{\"Strat\\u00E9gie\":\"4\",\"Influence\":\"0\"}}"
                        });
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
                            Password = "$2a$11$0cCo4Ciq8/0QZszDtAkP.eK969i/yEeK0bLIU3Tr8Zrut/BxkT5wS",
                            Role = 1
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Dragon",
                            Password = "$2a$11$1lokWFVxSB.CnBrMpKlFNOnKJm5w04ZCgEX4SBRyz83OYXU7XOqla",
                            Role = 0
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Grue",
                            Password = "$2a$11$A8cdb6KPnCYxTdEz42jzZ.UOCNlF/9jMg/KuVf6Dm0DdoFUU2N1Bu",
                            Role = 0
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Lion",
                            Password = "$2a$11$i3YhcWnD0DFn9mq/geDUD.XBqvWGqb1kn/7nJtPxFeX3vfxc3w8Ie",
                            Role = 0
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Scorpion",
                            Password = "$2a$11$.B24rVXy6wHIdKC1uF49dOzem5wFa4nFbD3PumlzAaXDnmhP67T6O",
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
#pragma warning restore 612, 618
        }
    }
}

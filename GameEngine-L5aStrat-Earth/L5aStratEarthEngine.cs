using Entities.Database;
using Entities.Enums;
using Entities.Interfaces;
using Entities.Models;
using L5aStrat_Earth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace L5aStrat_Earth
{
    public class L5aStratEarthEngine : IGameEngine
    {
        private readonly DAL _dal;
        private readonly ActionsEngine _actionsEngine;
        private readonly OptionsEngine _optionsEngine;
        private readonly ValidationEngine _validationEngine;

        public L5aStratEarthEngine(DAL dal)
        {
            _dal = dal;
            _actionsEngine = new ActionsEngine(_dal);
            _optionsEngine = new OptionsEngine(_dal);
            _validationEngine = new ValidationEngine(_dal);
        }

        public bool BeginTurn(Campaign campaign)
        {
            // Trouver l'id du joueur admin
            var players = (from p in _dal.Players
                           where p.CampaignId == campaign.Id
                           select p).ToList();
            var adminId = players.FirstOrDefault(p => p.IsAdmin).Id;

            // Générer la map de début de tour
            var newMap = new Map()
            {
                PlayerId = adminId,
                CampaignId = campaign.Id,
                Name = $"Début de Tour {campaign.CurrentTurn}",
                Turn = campaign.CurrentTurn,
                Size = 9,
                CreationDate = DateTime.Now
            };
            _dal.Maps.Add(newMap);
            _dal.SaveChanges();
            _dal.MapTiles.AddRange(this.GenerateMapTiles(newMap.Id, campaign.Id, adminId));
            foreach(var player in players)
            {
                player.HasNewMap = true;
            }

            // Générer les feuilles d'ordres des joueurs
            foreach (var player in players.Where(p => !p.IsAdmin))
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                var sheet = new OrdersSheet()
                {
                    MaxOrdersCount = 5,
                    MaxPriority = Math.Min(playerAssets.Resources.Strategy, 10),
                    PlayerId = player.Id,
                    Priority = 0,
                    Status = OrdersSheetStatus.Writing,
                    Turn = campaign.CurrentTurn
                };
                _dal.OrdersSheets.Add(sheet);
            }
            _dal.SaveChanges();

            return true;
        }

        public bool EndSheet(Player player)
        {
            var campaign = (from c in _dal.Campaigns
                          where c.Id == player.CampaignId
                          select c).FirstOrDefault();

            var armies = (from a in _dal.Units
                          where a.PlayerId == player.Id && a.Type == "Army"
                          select a).ToList();

            foreach(var army in armies)
            {
                var formation = army.Assets["Formation"];
                army.Assets = new Dictionary<string, Dictionary<string, string>>() { { "Formation", formation } };
            }

            var newMap = new Map()
            {
                PlayerId = player.Id,
                CampaignId = campaign.Id,
                Name = $"Retour d'ordres - Tour {campaign.CurrentTurn}",
                Turn = campaign.CurrentTurn,
                Size = 9,
                CreationDate = DateTime.Now
            };

            _dal.Maps.Add(newMap);
            _dal.SaveChanges();

            _dal.MapTiles.AddRange(this.GenerateMapTiles(newMap.Id, campaign.Id, player.Id));

            return true;
        }

        public bool EndTurn(Campaign campaign)
        {
            var result = true;

            // Gérer les productions des bâtiments
            var units = (from a in _dal.Units
                          join p in _dal.Players on a.PlayerId equals p.Id
                          where p.CampaignId == campaign.Id
                          select a).ToList();
            var armies = units.Where(u => u.Type == "Army");
            var buildings = units.Where(u => u.Type == "Building");

            var players = (from p in _dal.Players
                         where p.CampaignId == campaign.Id && !p.IsAdmin
                         select p).ToList();

            var gloryBonus = new Dictionary<long, int>();
            var stratBonus = new Dictionary<long, int>();
            var scoreBonus = new Dictionary<long, int>();
            foreach(var player in players)
            {
                gloryBonus.Add(player.Id, 0);
                stratBonus.Add(player.Id, 0);
                scoreBonus.Add(player.Id, 0);
            }
            foreach (var building in buildings)
            {
                var occupyingArmy = armies.Where(a => a.X == building.X && a.Y == building.Y).FirstOrDefault();
                if (occupyingArmy != null && building.Assets.ContainsKey("Type"))
                {
                    switch(building.Assets["Type"].Keys.First())
                    {
                        case "Village":
                            {
                                gloryBonus[occupyingArmy.PlayerId] += 2;
                                scoreBonus[occupyingArmy.PlayerId] += 10;
                                break;
                            }
                        case "Avant-Poste":
                            {
                                gloryBonus[occupyingArmy.PlayerId] += 1;
                                stratBonus[occupyingArmy.PlayerId] += 2;
                                scoreBonus[occupyingArmy.PlayerId] += 10;
                                break;
                            }
                        case "Forteresse":
                            {
                                gloryBonus[occupyingArmy.PlayerId] += 3;
                                stratBonus[occupyingArmy.PlayerId] += 2;
                                scoreBonus[occupyingArmy.PlayerId] += 20;
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            foreach(var player in players)
            {
                this.AddProduction(player, gloryBonus[player.Id], stratBonus[player.Id], campaign.CurrentTurn);
            }

            // Suppression des renforts non concrétisés
            var virtualUnits = units.Where(u => u.Type == "Reinforcement");
            _dal.Units.RemoveRange(virtualUnits);
            _dal.SaveChanges();

            // Expiration des feuilles d'ordres non rendues
            var expiredSheets = (from s in _dal.OrdersSheets
                                 join p in _dal.Players on s.PlayerId equals p.Id
                                 where p.CampaignId == campaign.Id && s.Status == OrdersSheetStatus.Writing
                                 select s).ToList();

            var planificationId = (from a in _dal.ActionTypes
                                   where a.Label == "Planification"
                                   select a.Id).FirstOrDefault();
            foreach (var sheet in expiredSheets)
            {
                sheet.Status = OrdersSheetStatus.Expired;
                sheet.SendDate = DateTime.Now;
                _dal.OrdersSheets.Update(sheet);
                var expiredOrders = (from o in _dal.Orders
                                     where o.OrdersSheetId == sheet.Id
                                     select o).ToList();
                _dal.Orders.RemoveRange(expiredOrders);
                for(int i=0; i<sheet.MaxOrdersCount; i++)
                {
                    var order = new Order()
                    {
                        ActionTypeId = planificationId,
                        Comment = "+1 Stratégie",
                        Rank = i,
                        Status = OrderStatus.Valid,
                        OrdersSheetId = sheet.Id,
                        Parameters = new Dictionary<string, string>()
                    };
                    this.ProcessOrder(order);
                    _dal.Orders.Add(order);
                }
            }

            // Gérer le classement
            if (campaign.CurrentTurn % 3 == 0)
            {
                var leaderboard = this.MakeLeaderboard(players, scoreBonus);
                if (campaign.CurrentTurn >= 12)
                {
                    campaign.Assets = new Dictionary<string, Dictionary<string, string>>() {
                        { "Classement final", leaderboard }
                    };
                }
                else
                {
                    campaign.Assets = new Dictionary<string, Dictionary<string, string>>() {
                        { "Classement", leaderboard },
                        { "Prochain classement", new Dictionary<string, string>() { { $"Tour {campaign.CurrentTurn + 3}", string.Empty } } }
                    };
                }
            }
            return result;
        }

        public Dictionary<string, string> GetOptionsList(string resource, long playerId, IEnumerable<string> parameters)
        {
            switch (resource)
            {
                case "Army":
                    {
                        return this._optionsEngine.GetArmies(playerId);
                    }
                case "EntryTile":
                    {
                        return this._optionsEngine.GetEntryTiles(playerId);
                    }
                case "Formation":
                    {
                        return this._optionsEngine.GetFormations(parameters.FirstOrDefault());
                    }
                case "Move":
                    {
                        return this._optionsEngine.GetMoves(parameters.FirstOrDefault(), playerId);
                    }
                case "Opponent":
                    {
                        return this._optionsEngine.GetOpponents(playerId);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public int PayPriority(Player player, int priority)
        {
            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (playerAssets.Resources.Strategy < priority) priority = playerAssets.Resources.Strategy;
            playerAssets.Resources.Strategy -= priority;
            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
            _dal.Players.Update(player);
            _dal.SaveChanges();

            return priority;
        }

        public Order ProcessOrder(Order order)
        {
            order.ActionType = (from a in _dal.ActionTypes
                          where a.Id == order.ActionTypeId
                          select a).FirstOrDefault();

            switch (order.ActionType.Label)
            {
                case "Déplacement":
                    {
                        return this._actionsEngine.Move(order);
                    }
                case "Flatterie":
                    {
                        return this._actionsEngine.Flatter(order);
                    }
                case "Formation":
                    {
                        return this._actionsEngine.Formation(order);
                    }
                case "Médisance":
                    {
                        return this._actionsEngine.Calomny(order);
                    }
                case "Planification":
                    {
                        return this._actionsEngine.Planify(order);
                    }
                case "Renforts":
                    {
                        return this._actionsEngine.Reinforce(order);
                    }
                case "Renseignements":
                    {
                        return this._actionsEngine.Spy(order);
                    }
                default:
                    {
                        order.Status = OrderStatus.Error;
                        order.Comment = "Type d'action inconnu";
                        return order;
                    }
            }
        }

        public void InitCampaign(Campaign campaign)
        {
            var players = (from p in _dal.Players
                           where p.CampaignId == campaign.Id
                           select p).ToList();

            var adminId = players.FirstOrDefault(p => p.IsAdmin).Id;
            var playerIds = players.Where(p => !p.IsAdmin).Select(p => p.Id).ToList();

            campaign.Assets = new Dictionary<string, Dictionary<string, string>>() {
                { "Prochain classement", new Dictionary<string, string>() { { "Tour 3", string.Empty } } }
            };

            foreach (var player in players)
            {
                player.Assets = new Dictionary<string, Dictionary<string, string>>()
                    {
                        { "Caractéristiques", new Dictionary<string, string> () {{"Gloire", "5"}, { "Infamie", "0" } } },
                        { "Ressources", new Dictionary<string, string> () {{"Stratégie", "5"}, { "Influence", "0" } } }
                    };

                var message = new Message()
                {
                    SenderId = adminId,
                    PlayerId = player.Id,
                    Subject = "Bienvenue",
                    Body = $"Bienvenue sur la partie '{campaign.Name}' ! Bon jeu à tous !",
                    IsNotification = true,
                    SendDate = DateTime.Now
                };

                _dal.Messages.Add(message);
            }

            _dal.Units.AddRange(new List<Unit>()
            {
                CreateBuilding("Forteresse", 4, 4, adminId),
                CreateBuilding("Avant-Poste", 1, 1, adminId),
                CreateBuilding("Avant-Poste", 7, 7, adminId),
                CreateBuilding("Village", 2, 3, adminId),
                CreateBuilding("Village", 3, 6, adminId),
                CreateBuilding("Village", 5, 2, adminId),
                CreateBuilding("Village", 6, 5, adminId),
                CreateBuilding("Entrée", 0, 3, playerIds[0]),
                CreateBuilding("Entrée", 0, 4, playerIds[0]),
                CreateBuilding("Entrée", 0, 5, playerIds[0]),
                CreateBuilding("Entrée", 0, 6, playerIds[0]),
                CreateBuilding("Entrée", 3, 0, playerIds[1]),
                CreateBuilding("Entrée", 4, 0, playerIds[1]),
                CreateBuilding("Entrée", 5, 0, playerIds[1]),
                CreateBuilding("Entrée", 6, 0, playerIds[1]),
                CreateBuilding("Entrée", 2, 8, playerIds[2]),
                CreateBuilding("Entrée", 3, 8, playerIds[2]),
                CreateBuilding("Entrée", 4, 8, playerIds[2]),
                CreateBuilding("Entrée", 5, 8, playerIds[2]),
                CreateBuilding("Entrée", 8, 2, playerIds[3]),
                CreateBuilding("Entrée", 8, 3, playerIds[3]),
                CreateBuilding("Entrée", 8, 4, playerIds[3]),
                CreateBuilding("Entrée", 8, 5, playerIds[3])
            });
        }

        private Unit CreateBuilding(string type, int x, int y, long playerId)
        {
            var newUnit = new Unit()
            {
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
            return newUnit;
        }

        private List<MapTile> GenerateMapTiles(long mapId, long campaignId, long playerId)
        {
            var result = new List<MapTile>();

            var players = (from p in _dal.Players
                           where p.CampaignId == campaignId
                           select p).ToList();

            var units = (from u in _dal.Units
                         join p in _dal.Players on u.PlayerId equals p.Id
                         where p.CampaignId == campaignId
                         select u).ToList();

            var actionTypes = (from a in _dal.ActionTypes
                               join c in _dal.Campaigns on a.GameId equals c.GameId
                               where c.Id == campaignId
                               select a).ToList();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var tileName = "Plaine";
                    var tileAssets = new Dictionary<string, Dictionary<string, string>>();
                    var tileParameters = new Dictionary<string, string>();
                    var tileColor = ((i + j) % 2 == 1) ? "linen" : "wheat";
                    var tileBorderColor = string.Empty;
                    var tileSymbol = string.Empty;

                    foreach (var unit in units.Where(u => u.X == i && u.Y == j && u.Type == "Building" && u.Assets.ContainsKey("Type")))
                    {
                        switch (unit.Assets["Type"].Keys.FirstOrDefault())
                        {
                            case "Entrée":
                                {
                                    tileName = "Entrée";
                                    tileSymbol = "home";
                                    tileBorderColor = players.FirstOrDefault(p => p.Id == unit.PlayerId).Color;
                                    tileAssets.Add("Point d'entrée", new Dictionary<string, string>() { { "Propriétaire", players.FirstOrDefault(p => p.Id == unit.PlayerId).Name } });
                                    break;
                                }
                            case "Village":
                                {
                                    tileName = "Village";
                                    tileSymbol = "house";
                                    tileBorderColor = "lightgrey";
                                    tileAssets.Add("Production", new Dictionary<string, string>() { { "Gloire", "2" } });
                                    break;
                                }
                            case "Avant-Poste":
                                {
                                    tileName = "Avant-Poste";
                                    tileSymbol = "tower";
                                    tileBorderColor = "lightgrey";
                                    tileAssets.Add("Production", new Dictionary<string, string>() { { "Gloire", "1" }, { "Stratégie", "2" } });
                                    break;
                                }
                            case "Forteresse":
                                {
                                    tileName = "Forteresse";
                                    tileSymbol = "castle";
                                    tileBorderColor = "lightgrey";
                                    tileAssets.Add("Production", new Dictionary<string, string>() { { "Gloire", "3" }, { "Stratégie", "2" } });
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    foreach (var unit in units.Where(u => u.X == i && u.Y == j && u.Type == "Army"))
                    {
                        tileColor = players.FirstOrDefault(p => p.Id == unit.PlayerId).Color;
                        var formation = "Inconnue";
                        if (unit.Assets.ContainsKey("Formation")
                            && units.Exists(u => u.X <= i+1 
                                            && u.X >= i - 1 
                                            && u.Y <= j + 1 
                                            && u.Y >= j - 1 
                                            && u.Type == "Army"
                                            && u.PlayerId == playerId))
                        {
                            formation = unit.Assets["Formation"].Keys.FirstOrDefault();
                        }
                        tileAssets.Add($"Armée - {unit.Name}", new Dictionary<string, string>() { { "Propriétaire", players.FirstOrDefault(p => p.Id == unit.PlayerId).Name }, { "Formation", formation } });
                        tileParameters.Add("Army", $"{unit.Id};{unit.Name}");
                    }

                    tileName += $" ({References.coordinatesLetters[i]}{j + 1})";
                    var mapTile = new MapTile()
                    {
                        MapId = mapId,
                        X = i,
                        Y = j,
                        Name = tileName,
                        BorderColor = tileBorderColor,
                        Color = tileColor,
                        Symbol = tileSymbol,
                        Assets = tileAssets,
                        Parameters = tileParameters
                    };

                    _dal.MapTiles.Add(mapTile);
                }
            }

            return result;
        }

        private bool AddProduction(Player player, int gloryValue, int stratValue, int turn)
        {
            if (gloryValue > 0)
            {
                Helper.AddGlory(player, gloryValue);
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                playerAssets.Resources.Strategy += stratValue;
                player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                _dal.Update(player);
                var body = $"Vos bâtiments vous rapportent {gloryValue} point{(gloryValue > 1 ? "s" : "")} de Gloire";
                if (stratValue > 0)
                { 
                    body += $" et {stratValue} points de Stratégie."; 
                }
                else body += ".";
                Helper.SendNotification(player.Id, $"Rapport de production du Tour {turn}", body, _dal);
            }
            return true;
        }

        private Dictionary<string, string> MakeLeaderboard(IEnumerable<Player> players, Dictionary<long, int> scoreBonus)
        {
            Dictionary<Player, int> scores = new Dictionary<Player, int>();
            foreach (var player in players)
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                var bonus = scoreBonus[player.Id];
                scores.Add(player, playerAssets.Attributes.Glory + bonus);
            }
            var leaderBoard = new Dictionary<string, string>();
            string position = "1er";
            int reward = 3;
            while(scores.Count > 0)
            {
                var bestScore = 0;
                foreach(var score in scores.Values)
                {
                    if (score > bestScore)
                        bestScore = score;
                }
                var treatedPlayers = new List<Player>();
                var notifSubject = $"Vous êtes {position} au classement.";
                foreach (var player in scores.Where(s => s.Value == bestScore).Select(s => s.Key))
                {
                    leaderBoard.Add(position, player.Name);
                    treatedPlayers.Add(player);
                    position += "=";
                }
                foreach(var player in treatedPlayers)
                {
                    if (reward > 0)
                    {
                        Helper.AddGlory(player, reward);
                        var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                        playerAssets.Resources.Strategy += reward;
                        player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                        _dal.Update(player);
                        Helper.SendNotification(player.Id, notifSubject, $"Vous gagnez {reward} point{(reward > 1 ? "s" : "")} de Gloire et de Stratégie.", _dal);
                    }
                    scores.Remove(player);
                }
                position = $"{players.Count() + 1 - scores.Count()}ème";
                if (scores.Count() == players.Count() - 1)
                    reward = 1;
                else
                    reward = 0;
            }
            return leaderBoard;
        }

        public List<Order> CheckOrdersSheet(OrdersSheet sheet)
        {
            // On récupère les infos des joueurs de la campagne
            var dbPlayer = (from p in _dal.Players
                           where p.Id == sheet.PlayerId
                           select p).FirstOrDefault();
            var player = new Player()
            {
                Id = dbPlayer.Id,
                Assets = dbPlayer.Assets,
                CampaignId = dbPlayer.CampaignId,
                Name = dbPlayer.Name
            };

            // On récupère les infos des armées du joueur
            var dbUnits = (from u in _dal.Units
                         join p in _dal.Players on u.PlayerId equals p.Id
                         where p.Id == dbPlayer.Id
                         select u).ToList();
            var units = new List<Unit>();
            foreach(var army in dbUnits.Where(u => u.Type == "Army"))
            {
                units.Add(new Unit()
                {
                    Id = army.Id,
                    Assets = army.Assets,
                    Name = army.Name,
                    PlayerId = army.PlayerId,
                    Type = army.Type,
                    X = army.X,
                    Y = army.Y
                });
            }

            // On récupère les ordres de la feuille d'ordres
            var orders = (from o in _dal.Orders
                           where o.OrdersSheetId == sheet.Id
                           orderby o.Rank
                           select o).ToList();

            // On nettoie les unités virtuelles des ordres de renforts supprimés
            List<Unit> expiredUnits = new List<Unit>();
            foreach (var virtualUnit in dbUnits.Where(u => u.Type == "Reinforcement"))
            {
                long orderId;
                if (virtualUnit.Assets.ContainsKey("OrderId") && long.TryParse(virtualUnit.Assets["OrderId"].Keys.FirstOrDefault(), out orderId))
                {
                    var order = orders.FirstOrDefault(o => o.Id == orderId);
                    if (order == null) expiredUnits.Add(virtualUnit);
                }
            }
            _dal.Units.RemoveRange(expiredUnits);

            // On contrôle la validité des PPs
            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (playerAssets.Resources.Strategy < sheet.Priority)
            {
                sheet.Priority = playerAssets.Resources.Strategy;
            }
            playerAssets.Resources.Strategy -= sheet.Priority;
            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);

            // On contrôle la validité de chaque ordre
            foreach (var order in orders)
            {
                order.ActionType = (from a in _dal.ActionTypes
                                    where a.Id == order.ActionTypeId
                                    select a).FirstOrDefault();
                switch (order.ActionType.Label)
                {
                    case "Déplacement":
                        {
                            this._validationEngine.Move(order, player, units);
                            break;
                        }
                    case "Flatterie":
                        {
                            this._validationEngine.Flatter(order, player);
                            break;
                        }
                    case "Formation":
                        {
                            this._validationEngine.Formation(order, player, units);
                            break;
                        }
                    case "Médisance":
                        {
                            this._validationEngine.Calomny(order, player);
                            break;
                        }
                    case "Planification":
                        {
                            this._validationEngine.Planify(order, player);
                            break;
                        }
                    case "Renforts":
                        {
                            this._validationEngine.Reinforce(order, player, units);
                            break;
                        }
                    case "Renseignements":
                        {
                            this._validationEngine.Spy(order, player);
                            break;
                        }
                    default:
                        {
                            order.Status = OrderStatus.Invalid;
                            order.Comment = "Type d'action inconnu";
                            break;
                        }
                }
            }
            return orders;
        }
    }
}

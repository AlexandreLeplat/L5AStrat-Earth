using Entities.Database;
using Entities.Enums;
using Entities.Models;
using L5aStrat_Earth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace L5aStrat_Earth
{
    public class ActionsEngine
    {
        private readonly DAL _dal;

        public ActionsEngine(DAL dal)
        {
            _dal = dal;
        }

        public Order Planify(Order order)
        {
            var player = (from p in _dal.Players
                          join s in _dal.OrdersSheets on p.Id equals s.PlayerId
                          where s.Id == order.OrdersSheetId
                          select p).FirstOrDefault();

            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                playerAssets.Attributes.Infamy += 2;
                playerAssets.Resources.Strategy += 2;
            }
            else
            {
                playerAssets.Resources.Strategy += 1;
            }

            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
            _dal.Players.Update(player);
            _dal.SaveChanges();

            order.Status = OrderStatus.Completed;
            return order;
        }

        public Order Flatter(Order order)
        {
            var player = (from p in _dal.Players
                          join s in _dal.OrdersSheets on p.Id equals s.PlayerId
                          join o in _dal.Orders on s.Id equals o.OrdersSheetId
                          where o.Id == order.Id
                          select p).FirstOrDefault();

            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Flatterie", "Vous n'avez pas assez d'Influence.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    Helper.AddGlory(player, 4);
                }
            }
            else
            {
                long targetId;
                if (!order.Parameters.ContainsKey("Cible")
                    || string.IsNullOrEmpty(order.Parameters["Cible"])
                    || !long.TryParse(order.Parameters["Cible"].Split(';')[0], out targetId))
                {
                    order.Comment = "Pas de cible définie";
                    order.Status = OrderStatus.Error;
                    return order;
                }
                var targetPlayer = (from p in _dal.Players
                                    where p.Id == targetId && p.CampaignId == player.CampaignId && p.Id != player.Id
                                    select p).FirstOrDefault();
                if (targetPlayer == null)
                {
                    order.Comment = "Cible invalide";
                    order.Status = OrderStatus.Error;
                    return order;
                }

                Helper.AddGlory(targetPlayer, 1);
                _dal.Players.Update(targetPlayer);
                Helper.AddGlory(player, 1);
                this.SendNotification(targetPlayer.Id, "On vous flatte à la Cour", $"{player.Name} vante vos mérites. Vous gagnez 1 point de Gloire.");
            }

            _dal.Players.Update(player);
            _dal.SaveChanges();

            order.Status = OrderStatus.Completed;
            return order;
        }

        public Order Trade(Order order)
        {
            var player = (from p in _dal.Players
                          join s in _dal.OrdersSheets on p.Id equals s.PlayerId
                          join o in _dal.Orders on s.Id equals o.OrdersSheetId
                          where o.Id == order.Id
                          select p).FirstOrDefault();

            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Commerce", "Vous n'avez pas assez d'Influence.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    playerAssets.Resources.Strategy += 5;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    Helper.AddInfamy(player, 1);
                }
            }
            else
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Strategy < 5)
                {
                    order.Comment = "Pas assez de points de Stratégie";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Commerce", "Vous n'avez pas assez de points de Stratégie.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Influence++;
                    playerAssets.Resources.Strategy -= 5;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    Helper.AddInfamy(player, 1);
                }
            }

            _dal.Players.Update(player);
            _dal.SaveChanges();

            order.Status = OrderStatus.Completed;
            return order;
        }

        public Order Calomny(Order order)
        {
            var player = (from p in _dal.Players
                          join s in _dal.OrdersSheets on p.Id equals s.PlayerId
                          join o in _dal.Orders on s.Id equals o.OrdersSheetId
                          where o.Id == order.Id
                          select p).FirstOrDefault();

            long targetId;
            if (!order.Parameters.ContainsKey("Cible")
                || string.IsNullOrEmpty(order.Parameters["Cible"])
                || !long.TryParse(order.Parameters["Cible"].Split(';')[0], out targetId))
            {
                order.Comment = "Pas de cible définie";
                order.Status = OrderStatus.Error;
                return order;
            }
            var targetPlayer = (from p in _dal.Players
                                where p.Id == targetId && p.CampaignId == player.CampaignId && p.Id != player.Id
                                select p).FirstOrDefault();
            if (targetPlayer == null)
            {
                order.Comment = "Cible invalide";
                order.Status = OrderStatus.Error;
                return order;
            }

            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Médisance", "Vous n'avez pas assez d'Influence.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    Helper.AddGlory(player, 1);
                    Helper.AddInfamy(targetPlayer, 6);
                    this.SendNotification(targetPlayer.Id, "Vous êtes la cible d'un scandale !", "Vous subissez 6 points d'Infamie.");
                }
            }
            else
            {
                Helper.AddInfamy(player, 1);
                Helper.AddInfamy(targetPlayer, 3);
                this.SendNotification(targetPlayer.Id, "Vous êtes la cible de médisances", "Vous subissez 3 points d'Infamie.");
            }

            _dal.Players.Update(targetPlayer);
            _dal.Players.Update(player);
            _dal.SaveChanges();

            order.Status = OrderStatus.Completed;
            return order;
        }

        public Order Spy(Order order)
        {
            var player = (from p in _dal.Players
                          join s in _dal.OrdersSheets on p.Id equals s.PlayerId
                          join o in _dal.Orders on s.Id equals o.OrdersSheetId
                          where o.Id == order.Id
                          select p).FirstOrDefault();

            var targets = new List<Player>();

            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Renseignements", "Vous n'avez pas assez d'Influence.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    targets = (from p in _dal.Players
                               where p.CampaignId == player.CampaignId && !p.IsAdmin && p.Id != player.Id
                               select p).ToList();
                }
            }
            else
            {
                long targetId;
                if (!order.Parameters.ContainsKey("Cible")
                    || string.IsNullOrEmpty(order.Parameters["Cible"])
                    || !long.TryParse(order.Parameters["Cible"].Split(';')[0], out targetId))
                {
                    order.Comment = "Pas de cible définie";
                    order.Status = OrderStatus.Error;
                    return order;
                }
                var targetPlayer = (from p in _dal.Players
                                    where p.Id == targetId && p.CampaignId == player.CampaignId && p.Id != player.Id
                                    select p).FirstOrDefault();
                if (targetPlayer == null)
                {
                    order.Comment = "Cible invalide";
                    order.Status = OrderStatus.Error;
                    return order;
                }
                targets.Add(targetPlayer);
                Helper.AddInfamy(player, 1);
            }

            var currentTurn = (from c in _dal.Campaigns
                               where c.Id == player.CampaignId
                               select c.CurrentTurn).FirstOrDefault();

            var adminId = (from p in _dal.Players
                           join p2 in _dal.Players on p.CampaignId equals p2.CampaignId
                           where p.IsAdmin && p2.Id == player.Id
                           select p.Id).FirstOrDefault();

            foreach (var target in targets)
            {
                string title = $"Renseignements {target.Name}";
                string report = string.Empty;
                foreach(var category in target.Assets.Keys)
                {
                    report += $"b§:{category} :{System.Environment.NewLine}";
                    foreach (var key in target.Assets[category].Keys)
                    {
                        if (!string.IsNullOrEmpty(target.Assets[category][key]))
                        {
                            report += $"- {key} : {target.Assets[category][key]}{System.Environment.NewLine}";
                        }
                        else
                        {
                            report += key + System.Environment.NewLine;
                        }
                    }
                }
                report += $"{System.Environment.NewLine}b§:Armées :{System.Environment.NewLine}";

                var units = (from u in _dal.Units
                             where u.PlayerId == target.Id && u.Type == "Army"
                             select u).ToList();
                foreach(var unit in units)
                {
                    report += $"- {unit.Name} ({References.coordinatesLetters[unit.X]}{unit.Y + 1})";
                    if (unit.Assets.Keys.Contains("Formation") && unit.Assets["Formation"].Any())
                        report += $" : {unit.Assets["Formation"].Keys.FirstOrDefault()}";
                    report += System.Environment.NewLine;
                }

                var lastOrderSheet = (from s in _dal.OrdersSheets
                                      where s.PlayerId == target.Id && s.Status == OrdersSheetStatus.Completed
                                      orderby s.SendDate descending
                                      select s).FirstOrDefault();

                if (lastOrderSheet != null)
                {
                    report += $"{System.Environment.NewLine}b§:Ordres Tour {lastOrderSheet.Turn} :{System.Environment.NewLine}";
                    
                    var orders = (from o in _dal.Orders
                                  where o.OrdersSheetId == lastOrderSheet.Id
                                  orderby o.Rank
                                  select o).ToList();
                    var actionTypes = (from a in _dal.ActionTypes
                                       select a).ToList();

                    foreach (var targetOrder in orders)
                    {
                        var actionType = actionTypes.FirstOrDefault(a => a.Id == targetOrder.ActionTypeId);
                        report += $"- {actionType.Label}";
                        if (!string.IsNullOrEmpty(targetOrder.Comment))
                        {
                            report += $" : {targetOrder.Comment}";
                        }
                        report += System.Environment.NewLine;
                    }
                }
                this.SendNotification(player.Id, title, report);
            }

            _dal.Players.Update(player);
            _dal.SaveChanges();

            order.Status = OrderStatus.Completed;
            return order;
        }

        public Order Formation(Order order)
        {
            var player = (from p in _dal.Players
                          join s in _dal.OrdersSheets on p.Id equals s.PlayerId
                          join o in _dal.Orders on s.Id equals o.OrdersSheetId
                          where o.Id == order.Id
                          select p).FirstOrDefault();

            long armyId;
            if (!order.Parameters.ContainsKey("Armée")
                || string.IsNullOrEmpty(order.Parameters["Armée"])
                || !long.TryParse(order.Parameters["Armée"].Split(';')[0], out armyId))
            {
                order.Comment = "Pas d'armée définie";
                order.Status = OrderStatus.Error;
                return order;
            }
            var army = (from u in _dal.Units
                                where u.Id == armyId && u.PlayerId == player.Id
                                select u).FirstOrDefault();
            if (army == null)
            {
                order.Comment = "Armée introuvable";
                order.Status = OrderStatus.Failed;
                this.SendNotification(player.Id, "Ordre annulé : Formation", "L'armée ciblée n'existe pas.");
                return order;
            }

            var armyAssets = new Dictionary<string, Dictionary<string, string>>();
            foreach (var a in army.Assets) { armyAssets.Add(a.Key, a.Value); }
            if (army.Assets.ContainsKey("HasChangedFormation"))
            {
                order.Comment = "L'armée a déjà changé de formation";
                order.Status = OrderStatus.Failed;
                this.SendNotification(player.Id, "Ordre annulé : Formation", order.Comment);
                return order;
            }
            else
            {
                armyAssets.Add("HasChangedFormation", new Dictionary<string, string>());
                army.Assets = armyAssets;
            }

            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                if (playerAssets.Resources.Strategy < 1)
                {
                    order.Comment = "Pas assez de points de Stratégie";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Formation", "Vous n'avez pas assez de points de Stratégie.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Strategy--;
                }
            }
            else
            {
                if (army.Assets.ContainsKey("HasMoved"))
                {
                    order.Comment = "Armée déjà déplacée";
                    order.Status = OrderStatus.Failed;
                    return order;
                }
            }
            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);

            var formation = "Choki";
            if (order.Parameters.ContainsKey("Formation") && References.FormationList.Contains(order.Parameters["Formation"]))
            {
                formation = order.Parameters["Formation"];
            }

            if (armyAssets.ContainsKey("Formation"))
            {
                armyAssets.Remove("Formation");
            }
            armyAssets.Add("Formation", new Dictionary<string, string>() { { formation, null } });
            army.Assets = armyAssets;

            _dal.Players.Update(player);
            _dal.Units.Update(army);
            _dal.SaveChanges();

            order.Status = OrderStatus.Completed;
            return order;
        }

        public Order Reinforce(Order order)
        {
            var player = (from p in _dal.Players
                          join s in _dal.OrdersSheets on p.Id equals s.PlayerId
                          join o in _dal.Orders on s.Id equals o.OrdersSheetId
                          where o.Id == order.Id
                          select p).FirstOrDefault();

            long targetId;
            if (!order.Parameters.ContainsKey("Case")
                || string.IsNullOrEmpty(order.Parameters["Case"])
                || !long.TryParse(order.Parameters["Case"].Split(';')[0], out targetId))
            {
                order.Comment = "Pas de destination définie";
                order.Status = OrderStatus.Error;
                return order;
            }

            var destinationTile = (from t in _dal.MapTiles
                                   join m in _dal.Maps on t.MapId equals m.Id
                                   where t.Id == targetId && m.CampaignId == player.CampaignId
                                   select t).FirstOrDefault();
            if (destinationTile == null)
            {
                order.Comment = "Destination introuvable";
                order.Status = OrderStatus.Error;
                return order;
            }

            var building = (from b in _dal.Units
                            where b.PlayerId == player.Id && b.X == destinationTile.X && b.Y == destinationTile.Y && b.Type == "Building"
                            select b).FirstOrDefault();
            if (building == null)
            {
                order.Comment = "Case de destination invalide";
                order.Status = OrderStatus.Invalid;
                return order;
            }

            var occupiedTile = (from u in _dal.Units
                                join p in _dal.Players on u.PlayerId equals p.Id
                                where p.CampaignId == player.CampaignId && u.X == destinationTile.X && u.Y == destinationTile.Y && u.Type == "Army"
                                select u).FirstOrDefault();
            if (occupiedTile != null)
            {
                order.Comment = "Case de destination occupée";
                order.Status = OrderStatus.Failed;
                this.SendNotification(player.Id, "Ordre annulé : Renforts", "La case de destination est occupée.");
                return order;
            }

            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Renforts", "Vous n'avez pas assez d'Influence.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                }
            }
            else
            {
                var strategyCost = 5;
                if (building.Assets.ContainsKey("Type"))
                {
                    if (building.Assets["Type"].ContainsKey("Entrée")) strategyCost = 4;
                    if (building.Assets["Type"].ContainsKey("Village")) strategyCost = 6;
                }

                if (playerAssets.Resources.Strategy < strategyCost)
                {
                    order.Comment = "Pas assez de points de Stratégie";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Renforts", "Vous n'avez pas assez de points de Stratégie.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Strategy = playerAssets.Resources.Strategy - strategyCost;
                }
            }
            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);

            var formation = "Choki";
            if (order.Parameters.ContainsKey("Formation") && References.FormationList.Contains(order.Parameters["Formation"]))
            {
                formation = order.Parameters["Formation"];
            }

            var virtualUnits = (from u in _dal.Units
                                where u.PlayerId == player.Id && u.Type == "Reinforcement"
                                select u).ToList();
            var virtualUnit = virtualUnits.FirstOrDefault(u => u.Assets.ContainsKey("OrderId") && u.Assets["OrderId"].ContainsKey(order.Id.ToString()));
            if (virtualUnit == null)
            {
                order.Comment = "Renfort introuvable";
                order.Status = OrderStatus.Error;
                return order;
            }
            virtualUnit.Type = "Army";
            virtualUnit.X = destinationTile.X;
            virtualUnit.Y = destinationTile.Y;
            virtualUnit.Assets = new Dictionary<string, Dictionary<string, string>>()
            {
                { "Formation", new Dictionary<string, string>() { { formation, null } } },
                { "Renown", new Dictionary<string, string>() { { "3", null } } },
                { "HasReinforced", new Dictionary<string, string>() }
            };

            _dal.Players.Update(player);
            _dal.Units.Update(virtualUnit);
            _dal.SaveChanges();

            order.Status = OrderStatus.Completed;
            return order;
        }

        public Order Move(Order order)
        {
            var player = (from p in _dal.Players
                          join s in _dal.OrdersSheets on p.Id equals s.PlayerId
                          join o in _dal.Orders on s.Id equals o.OrdersSheetId
                          where o.Id == order.Id
                          select p).FirstOrDefault();

            long armyId;
            if (!order.Parameters.ContainsKey("Armée")
                || string.IsNullOrEmpty(order.Parameters["Armée"]) 
                || !long.TryParse(order.Parameters["Armée"].Split(';')[0], out armyId))
            {
                order.Comment = "Pas d'armée définie";
                order.Status = OrderStatus.Error;
                return order;
            }
            var army = (from u in _dal.Units
                                   where u.Id == armyId && u.PlayerId == player.Id && u.Type == "Army"
                                   select u).FirstOrDefault();
            if (army == null)
            {
                order.Comment = "Armée introuvable";
                order.Status = OrderStatus.Failed;
                this.SendNotification(player.Id, "Ordre annulé : Déplacement", "L'armée ciblée n'existe pas.");
                return order;
            }

            long targetId;
            if (!order.Parameters.ContainsKey("Destination")
                || string.IsNullOrEmpty(order.Parameters["Destination"])
                || !long.TryParse(order.Parameters["Destination"].Split(';')[0], out targetId))
            {
                order.Comment = "Pas de destination définie";
                order.Status = OrderStatus.Error;
                return order;
            }

            var destinationTile = (from t in _dal.MapTiles
                                   join m in _dal.Maps on t.MapId equals m.Id
                                   where t.Id == targetId && m.CampaignId == player.CampaignId
                                        && t.X >= army.X-1 && t.X <= army.X + 1
                                        && t.Y >= army.Y - 1 && t.Y <= army.Y + 1
                                   select t).FirstOrDefault();
            if (destinationTile == null)
            {
                order.Comment = "Destination inaccessible";
                order.Status = OrderStatus.Error;
                return order;
            }

            var armyAssets = new Dictionary<string, Dictionary<string, string>>();
            foreach (var a in army.Assets) { armyAssets.Add(a.Key, a.Value); }
            var armyActionCount = 0;
            if (armyAssets.ContainsKey("HasChangedFormation")) armyActionCount++;
            if (armyAssets.ContainsKey("HasReinforced")) armyActionCount++;
            if (armyAssets.ContainsKey("HasMoved")) armyActionCount++;

            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                if (armyActionCount > 1 || army.Assets.ContainsKey("HasForcedMarched"))
                {
                    order.Comment = "L'armée a déjà effectué plusieurs actions dans le tour";
                    order.Status = OrderStatus.Invalid;
                    return order;
                }
                if (playerAssets.Resources.Strategy < 5)
                {
                    order.Comment = "Pas assez de points de Stratégie";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Déplacement", "Vous n'avez pas assez de points de Stratégie.");
                    return order;
                }
                else
                {
                    playerAssets.Resources.Strategy -= 5;
                }
            }
            else
            {
                if (army.Assets.ContainsKey("HasChangedFormation") || army.Assets.ContainsKey("HasReinforced"))
                {
                    order.Comment = "L'armée a été créée dans le tour ou a déjà changé de formation.";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Déplacement", order.Comment);
                    return order;
                }
            }
            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
            _dal.Players.Update(player);

            var occupiedTile = (from u in _dal.Units
                                join p in _dal.Players on u.PlayerId equals p.Id
                                where u.X == destinationTile.X && u.Y == destinationTile.Y && u.Type == "Army" && p.CampaignId == player.CampaignId
                                select u).FirstOrDefault();
            if (occupiedTile != null)
            {
                if (occupiedTile.PlayerId == player.Id)
                {
                    order.Comment = "Case de destination occupée par un allié";
                    order.Status = OrderStatus.Failed;
                    this.SendNotification(player.Id, "Ordre annulé : Déplacement", order.Comment);
                    return order;
                }
                this.Battle(army, occupiedTile);
            }
            else
            {
                army.X = destinationTile.X;
                army.Y = destinationTile.Y;
                if (army.Assets.ContainsKey("HasMoved"))
                {
                    armyAssets.Add("HasForcedMarched", new Dictionary<string, string>());
                }
                else
                {
                    armyAssets.Add("HasMoved", new Dictionary<string, string>());
                }
                army.Assets = armyAssets;
                _dal.Units.Update(army);
            }

            // Prise de contrôle de bâtiment
            var building = (from b in _dal.Units
                             where b.Type == "Building" && b.X == army.X && b.Y == army.Y
                             select b).FirstOrDefault();
            if (building != null && building.PlayerId != army.PlayerId)
            {
                Helper.SendNotification(building.PlayerId, "Vous perdez un bâtiment", $"Une armée de {player.Name} prend le contrôle de votre {building.Name} en {References.coordinatesLetters[building.X]}{building.Y + 1}.", _dal);
                building.PlayerId = army.PlayerId;
                _dal.Units.Update(building);
            }

            _dal.SaveChanges();

            order.Status = OrderStatus.Completed;
            return order;
        }

        #region private
        private void Battle(Unit attacker, Unit defender)
        {
            Unit winner = null;
            var attackerName = (from p in _dal.Players
                           where p.Id == attacker.PlayerId
                           select p.Name).FirstOrDefault();
            var defenderName = (from p in _dal.Players
                                where p.Id == defender.PlayerId
                                select p.Name).FirstOrDefault();
            var adminId = (from p in _dal.Players
                           join p2 in _dal.Players on p.CampaignId equals p2.CampaignId
                           where p.IsAdmin && p2.Id == attacker.PlayerId
                           select p.Id).FirstOrDefault();
            var tileName = (from t in _dal.MapTiles
                                join m in _dal.Maps on t.MapId equals m.Id
                                where t.X == defender.X && t.Y == defender.Y && m.PlayerId == adminId
                            select t.Name).FirstOrDefault();
            
            string attackerNotifSubject = string.Empty;
            string defenderNotifSubject = string.Empty;
            string attackerNotifBody = string.Empty;
            string defenderNotifBody = string.Empty;

            if (attacker.Assets.ContainsKey("HasMoved"))
            {
                winner = defender;
            }
            else if (attacker.Assets["Formation"].ContainsKey("Choki"))
            {
                if (defender.Assets["Formation"].ContainsKey("Guu"))
                {
                    winner = defender;
                }
                else
                {
                    winner = attacker;
                }
            }
            else if (attacker.Assets["Formation"].ContainsKey("Guu") && defender.Assets["Formation"].ContainsKey("Paa"))
            {
                winner = defender;
            }
            else if (attacker.Assets["Formation"].ContainsKey("Paa"))
            {
                if (defender.Assets["Formation"].ContainsKey("Guu"))
                {
                    winner = attacker;
                }
                else if (defender.Assets["Formation"].ContainsKey("Choki"))
                {
                    winner = defender;
                }
            }

            if (winner != null)
            {
                var winnerAssets = new Dictionary<string, Dictionary<string, string>>();
                foreach (var a in winner.Assets) { winnerAssets.Add(a.Key, a.Value); }
                int renown = 0;
                if (winnerAssets.ContainsKey("Renown") && int.TryParse(winnerAssets["Renown"].Keys.First(), out renown))
                {
                    renown += 1;
                    winnerAssets["Renown"] = new Dictionary<string, string>() { { renown.ToString(), null } };
                }
                if (winner == attacker)
                {
                    attacker.X = defender.X;
                    attacker.Y = defender.Y;
                    winnerAssets.Add("HasMoved", new Dictionary<string, string>());
                    attacker.Assets = winnerAssets;
                    _dal.Update(attacker);
                    _dal.Remove(defender);
                    attackerNotifSubject = $"Victoire en {tileName}";
                    attackerNotifBody = $"Vous avez vaincu l'armée de {defenderName}, qui était en formation {defender.Assets["Formation"].Keys.FirstOrDefault()}.{System.Environment.NewLine}Vous gagnez 3 points de Gloire et 1 point de Stratégie.";
                    defenderNotifSubject = $"Défaite en {tileName}";
                    defenderNotifBody = $"Votre armée a été détruite par des troupes de {attackerName}, qui étaient en formation {attacker.Assets["Formation"].Keys.FirstOrDefault()}.";
                }
                else
                {
                    defender.Assets = winnerAssets;
                    _dal.Update(attacker);
                    _dal.Remove(attacker);
                    attackerNotifSubject = $"Défaite en {tileName}";
                    attackerNotifBody = $"Votre armée a été détruite par des troupes de {defenderName}, qui étaient en formation {defender.Assets["Formation"].Keys.FirstOrDefault()}.";
                    defenderNotifSubject = $"Victoire en {tileName}";
                    defenderNotifBody = $"Vous avez vaincu l'armée de {attackerName}, qui vous attaquait en formation {attacker.Assets["Formation"].Keys.FirstOrDefault()}.{System.Environment.NewLine}Vous gagnez 3 points de Gloire et 1 point de Stratégie.";
                }

                var player = (from p in _dal.Players
                              where p.Id == winner.PlayerId
                              select p).FirstOrDefault();

                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                playerAssets.Attributes.Glory += 3;
                playerAssets.Resources.Strategy++;
                player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                _dal.Players.Update(player);
            }
            else
            {
                attackerNotifSubject = $"Retraite en {tileName}";
                attackerNotifBody = $"Votre armée a été repoussée par les troupes de {defenderName}, qui étaient en formation {defender.Assets["Formation"].Keys.FirstOrDefault()}.";
                defenderNotifSubject = $"Attaquant repoussé en {tileName}";
                defenderNotifBody = $"Votre armée a tenu à distance les troupes de {attackerName}, qui étaient en formation {attacker.Assets["Formation"].Keys.FirstOrDefault()}.";
            }
            this.SendNotification(attacker.PlayerId, attackerNotifSubject, attackerNotifBody);
            this.SendNotification(defender.PlayerId, defenderNotifSubject, defenderNotifBody);
            _dal.SaveChanges();
        }

        private void SendNotification(long playerId, string subject, string body)
        {
            Helper.SendNotification(playerId, subject, body, _dal);
        }
        #endregion
    }
}

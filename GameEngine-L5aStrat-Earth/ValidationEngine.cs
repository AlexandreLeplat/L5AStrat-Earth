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
    public class ValidationEngine
    {
        private readonly DAL _dal;

        public ValidationEngine(DAL dal)
        {
            _dal = dal;
        }

        public Order Planify(Order order, Player player)
        {
            order.Status = OrderStatus.Valid;
            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                playerAssets.Attributes.Infamy += 2;
                playerAssets.Resources.Strategy += 2;
                order.Comment = "+2 Stratégie, +2 Infamie";
            }
            else
            {
                playerAssets.Resources.Strategy += 1;
                order.Comment = "+1 Stratégie";
            }

            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
            return order;
        }

        public Order Flatter(Order order, Player player)
        {
            order.Status = OrderStatus.Valid;
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence ?";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    order.Comment = "+4 Gloire, -1 Influence";
                }
                Helper.AddGlory(player, 4);
            }
            else
            {
                long targetId;
                if (!order.Parameters.ContainsKey("Cible")
                    || string.IsNullOrEmpty(order.Parameters["Cible"])
                    || !long.TryParse(order.Parameters["Cible"].Split(';')[0], out targetId))
                {
                    order.Comment = "Pas de cible définie";
                    order.Status = OrderStatus.Invalid;
                    return order;
                }
                var targetName = (from p in _dal.Players
                                  where p.Id == targetId && p.CampaignId == player.CampaignId && p.Id != player.Id
                                  select p.Name).FirstOrDefault();
                if (targetName == null)
                {
                    order.Comment = "Cible invalide";
                    order.Status = OrderStatus.Error;
                    return order;
                }

                order.Comment = $"+1 Gloire, {targetName} +1";
                Helper.AddGlory(player, 1);
            }

            return order;
        }

        public Order Trade(Order order, Player player)
        {
            order.Status = OrderStatus.Valid;
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence ?";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    order.Comment = "+5 Stratégie, +1 Infamie, -1 Influence";
                }
                playerAssets.Resources.Strategy += 5;
                player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                Helper.AddInfamy(player, 1);
            }
            else
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Strategy < 5)
                {
                    order.Comment = "Pas assez de points de Stratégie ?";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Strategy -= 5;
                    order.Comment = "-5 Stratégie, +1 Infamie, +1 Influence";
                }
                playerAssets.Resources.Influence++;
                player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                Helper.AddInfamy(player, 1);
            }

            return order;
        }
        
        public Order Calomny(Order order, Player player)
        {
            order.Status = OrderStatus.Valid;
            long targetId;
            if (!order.Parameters.ContainsKey("Cible")
                || string.IsNullOrEmpty(order.Parameters["Cible"])
                || !long.TryParse(order.Parameters["Cible"].Split(';')[0], out targetId))
            {
                order.Comment = "Pas de cible définie";
                order.Status = OrderStatus.Invalid;
                return order;
            }
            var targetName = (from p in _dal.Players
                              where p.Id == targetId && p.CampaignId == player.CampaignId && p.Id != player.Id
                              select p.Name).FirstOrDefault();
            if (targetName == null)
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
                    order.Comment = "Pas assez d'Influence ?";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    order.Comment = $"+1 Gloire, {targetName} +6 Infamie";
                }
                Helper.AddGlory(player, 1);
            }
            else
            {
                Helper.AddInfamy(player, 1);
                order.Comment = $"+1 Infamie, {targetName} +3";
            }

            return order;
        }

        public Order Spy(Order order, Player player)
        {
            order.Status = OrderStatus.Valid;
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence ?";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    order.Comment = "[Tous adversaires]";
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
                var targetName = (from p in _dal.Players
                                  where p.Id == targetId && p.CampaignId == player.CampaignId && p.Id != player.Id
                                  select p.Name).FirstOrDefault();
                if (targetName == null)
                {
                    order.Comment = "Cible invalide";
                    order.Status = OrderStatus.Error;
                    return order;
                }

                Helper.AddInfamy(player, 1);
                order.Comment = $"+1 Infamie, [{targetName}]";
            }

            return order;
        }

        public Order Formation(Order order, Player player, List<Unit> units)
        {
            order.Status = OrderStatus.Valid;
            long armyId;
            if (!order.Parameters.ContainsKey("Armée")
                || string.IsNullOrEmpty(order.Parameters["Armée"])
                || !long.TryParse(order.Parameters["Armée"].Split(';')[0], out armyId))
            {
                order.Comment = "Pas d'armée définie";
                order.Status = OrderStatus.Invalid;
                return order;
            }
            var army = units.FirstOrDefault(u => u.Id == armyId);
            if (army == null)
            {
                order.Comment = "Armée introuvable";
                order.Status = OrderStatus.Invalid;
                return order;
            }

            if (army.Assets.ContainsKey("HasChangedFormation"))
            {
                order.Comment = "L'armée a déjà changé de formation";
                order.Status = OrderStatus.Invalid;
                return order;
            }

            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                if (playerAssets.Resources.Strategy < 1)
                {
                    order.Comment = "Pas assez de points de Stratégie";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Strategy--;
                    order.Comment = "-1 Influence, ";
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
                order.Comment = string.Empty;
            }
            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);

            var formation = "Choki";
            if (order.Parameters.ContainsKey("Formation") && References.FormationList.Contains(order.Parameters["Formation"]))
            {
                formation = order.Parameters["Formation"];
            }

            if (order.Status == OrderStatus.Valid)
            {
                order.Comment += $"{army.Name} [{formation}]";
            }
            var armyAssets = new Dictionary<string, Dictionary<string, string>>();
            foreach (var a in army.Assets) { armyAssets.Add(a.Key, a.Value); }
            armyAssets.Add("HasChangedFormation", new Dictionary<string, string>());
            army.Assets = armyAssets;

            return order;
        }

        public Order Reinforce(Order order, Player player, List<Unit> units)
        {
            order.Status = OrderStatus.Valid;
            long targetId;
            if (!order.Parameters.ContainsKey("Case")
                || string.IsNullOrEmpty(order.Parameters["Case"])
                || !long.TryParse(order.Parameters["Case"].Split(';')[0], out targetId))
            {
                order.Comment = "Pas de destination définie";
                order.Status = OrderStatus.Invalid;
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

            var occupiedTile = units.FirstOrDefault(u => u.X == destinationTile.X && u.Y == destinationTile.Y);
            if (occupiedTile != null)
            {
                order.Comment = "Case de destination occupée";
                order.Status = OrderStatus.Invalid;
                return order;
            }

            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            if (order.Parameters.ContainsKey("Augmentation") && bool.Parse(order.Parameters["Augmentation"]))
            {
                if (playerAssets.Resources.Influence < 1)
                {
                    order.Comment = "Pas assez d'Influence ?";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Influence--;
                    order.Comment = "-1 Influence, ";
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
                    order.Comment = "Pas assez de points de Stratégie ?";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Strategy = playerAssets.Resources.Strategy - strategyCost;
                    order.Comment = $"-{strategyCost} Stratégie, ";
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
                var unitNames = (from u in _dal.Units
                                 join p in _dal.Players on u.PlayerId equals p.Id
                                 where p.CampaignId == player.CampaignId && (u.Type == "Army" || u.Type == "Reinforcement")
                                 select new Tuple<long, string>(u.PlayerId, u.Name)).ToList();
                var chosenName = Helper.ChooseRandomArmyName(unitNames.Where(n => n.Item1 == player.Id).Select(n => n.Item2), unitNames.Select(n => n.Item2));

                virtualUnit = new Unit()
                {
                    PlayerId = player.Id,
                    Name = chosenName,
                    Type = "Reinforcement"
                };
                _dal.Units.Add(virtualUnit);
                _dal.SaveChanges();
            }
            virtualUnit.X = destinationTile.X;
            virtualUnit.Y = destinationTile.Y;
            virtualUnit.Assets = new Dictionary<string, Dictionary<string, string>>()
            {
                { "Formation", new Dictionary<string, string>() { { formation, null } }  },
                { "HasReinforced", new Dictionary<string, string>() },
                { "OrderId", new Dictionary<string, string>() { { order.Id.ToString(), null } } }
            };
            units.Add(virtualUnit);
            _dal.Units.Update(virtualUnit);

            if (order.Status == OrderStatus.Valid)
            {
                order.Comment += $"{virtualUnit.Name} [{formation}] -> {References.coordinatesLetters[virtualUnit.X]}{virtualUnit.Y + 1}";
            }
            return order;
        }

        public Order Move(Order order, Player player, List<Unit> units)
        {
            order.Status = OrderStatus.Valid;
            long armyId;
            if (!order.Parameters.ContainsKey("Armée")
                || string.IsNullOrEmpty(order.Parameters["Armée"])
                || !long.TryParse(order.Parameters["Armée"].Split(';')[0], out armyId))
            {
                order.Comment = "Pas d'armée définie";
                order.Status = OrderStatus.Invalid;
                return order;
            }
            var army = units.FirstOrDefault(u => u.Id == armyId);
            if (army == null)
            {
                order.Comment = "Armée introuvable";
                order.Status = OrderStatus.Invalid;
                return order;
            }

            long targetId;
            if (!order.Parameters.ContainsKey("Destination")
                || string.IsNullOrEmpty(order.Parameters["Destination"])
                || !long.TryParse(order.Parameters["Destination"].Split(';')[0], out targetId))
            {
                order.Comment = "Pas de destination définie";
                order.Status = OrderStatus.Invalid;
                return order;
            }

            var destinationTile = (from t in _dal.MapTiles
                                   join m in _dal.Maps on t.MapId equals m.Id
                                   where t.Id == targetId && m.CampaignId == player.CampaignId
                                        && t.X >= army.X - 1 && t.X <= army.X + 1
                                        && t.Y >= army.Y - 1 && t.Y <= army.Y + 1
                                   select t).FirstOrDefault();
            if (destinationTile == null)
            {
                order.Comment = "Destination inaccessible";
                order.Status = OrderStatus.Invalid;
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
                    order.Comment = "Pas assez de points de Stratégie ?";
                    order.Status = OrderStatus.Invalid;
                }
                else
                {
                    playerAssets.Resources.Strategy -= 5;
                    player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
                    order.Comment = "-5 Stratégie, ";
                }
            }
            else
            {
                if (armyActionCount > 0)
                {
                    order.Comment = "L'armée a déjà effectué une action dans le tour";
                    order.Status = OrderStatus.Invalid;
                    return order;
                }
                order.Comment = string.Empty;
            }

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

            if (order.Status == OrderStatus.Valid)
            {
                order.Comment += $"{army.Name} -> {References.coordinatesLetters[army.X]}{army.Y + 1}";
            }
            return order;
        }
    }
}

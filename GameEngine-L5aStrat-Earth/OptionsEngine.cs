using Entities.Database;
using L5aStrat_Earth.Entities;
using System.Collections.Generic;
using System.Linq;

namespace L5aStrat_Earth
{
    public class OptionsEngine
    {
        private readonly DAL _dal;

        public OptionsEngine(DAL dal)
        {
            _dal = dal;
        }

        public Dictionary<string, string> GetFormations(string unitId)
        {
            var currentFormation = string.Empty;
            long id;
            if (long.TryParse(unitId, out id))
            {
                var army = (from a in _dal.Units
                            where a.Id == id
                            select a).FirstOrDefault();

                currentFormation = army.Assets.ContainsKey("Formation") ? army.Assets["Formation"].Keys.FirstOrDefault() : string.Empty;
            }

            var result = new Dictionary<string, string>();
            foreach(var formation in References.FormationList)
            {
                if (formation != currentFormation)
                {
                    result.Add(formation, formation);
                }
            }
            return result;
        }

        public Dictionary<string, string> GetEntryTiles(long playerId)
        {
            var result = new Dictionary<string, string>();
            var adminId = (from p1 in _dal.Players
                           join p2 in _dal.Players on p1.CampaignId equals p2.CampaignId
                           where p1.Id == playerId && p2.IsAdmin
                           select p2.Id).FirstOrDefault();

            var currentMapId = (from m in _dal.Maps
                                where m.PlayerId == playerId || m.PlayerId == adminId
                                orderby m.CreationDate descending
                                select m.Id).FirstOrDefault();

            var buildingTiles = (from t in _dal.MapTiles
                              join m in _dal.Maps on t.MapId equals m.Id
                              join u in _dal.Units on m.Id equals currentMapId
                              where u.Type == "Building" && u.PlayerId == playerId && t.X == u.X && t.Y == u.Y
                              select t).ToList();

            buildingTiles.ForEach(t =>
            {
                result.Add(t.Name, $"{t.Id};{t.Name}");
            });

            return result;
        }

        public Dictionary<string, string> GetMoves(string unitId, long playerId)
        {
            var result = new Dictionary<string, string>();
            long id;
            if (long.TryParse(unitId, out id))
            {
                var unit = (from u in _dal.Units
                        where u.Id == id && u.PlayerId == playerId
                        select u).FirstOrDefault();

                var adminId = (from p1 in _dal.Players
                               join p2 in _dal.Players on p1.CampaignId equals p2.CampaignId
                               where p1.Id == playerId && p2.IsAdmin
                               select p2.Id).FirstOrDefault();

                var currentMapId = (from m in _dal.Maps
                                    where m.PlayerId == playerId || m.PlayerId == adminId
                                    orderby m.CreationDate descending
                                    select m.Id).FirstOrDefault();

                if (unit == null || currentMapId == 0)
                {
                    return result;
                }

                var adjacentTiles = (from t in _dal.MapTiles
                                     where t.MapId == currentMapId
                                        && !(t.X == unit.X && t.Y == unit.Y)
                                        && t.X >= unit.X - 1 && t.X <= unit.X + 1 
                                        && t.Y >= unit.Y - 1 && t.Y <= unit.Y + 1
                                     select t).ToList();

                adjacentTiles.ForEach(t => result.Add(t.Name, $"{t.Id};{t.Name}"));
            }

            return result;
        }

        public Dictionary<string, string> GetOpponents(long playerId)
        {
            var result = new Dictionary<string, string>();

            var opponents = (from p in _dal.Players
                                join o in _dal.Players on p.CampaignId equals o.CampaignId
                                where p.Id == playerId && o.Id != playerId && !o.IsAdmin
                                select o).ToList();

            opponents.ForEach(o => result.Add(o.Name, $"{o.Id};{o.Name}"));

            return result;
        }

        public Dictionary<string, string> GetArmies(long playerId)
        {
            var result = new Dictionary<string, string>();

            var armies = (from u in _dal.Units
                                where (u.Type == "Army" || u.Type == "Reinforcement") && u.PlayerId == playerId
                          select u).ToList();

            armies.ForEach(a => result.Add($"{a.Name} ({References.coordinatesLetters[a.X]}{a.Y + 1})"
                                         , $"{a.Id};{a.Name} ({References.coordinatesLetters[a.X]}{a.Y + 1})"));

            return result;
        }
    }
}

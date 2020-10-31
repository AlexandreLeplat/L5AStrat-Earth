using Entities.Models;
using L5aStrat_Earth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace L5aStrat_Earth
{
    public static class Helper
    {
        public static void AddGlory(Player player, int bonus)
        {
            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            var currentInfluenceThreshold = (int)Math.Floor((double)playerAssets.Attributes.Glory / 10);
            if (playerAssets.Attributes.Infamy > bonus)
            {
                playerAssets.Attributes.Infamy -= bonus;
                bonus = 0;
            }
            else
            {
                bonus -= playerAssets.Attributes.Infamy;
                playerAssets.Attributes.Infamy = 0;
            }
            playerAssets.Attributes.Glory += bonus;
            playerAssets.Resources.Influence += (int)Math.Floor((double)playerAssets.Attributes.Glory / 10) - currentInfluenceThreshold;

            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
        }

        public static void AddInfamy(Player player, int malus)
        {
            var playerAssets = JsonSerializer.Deserialize<PlayerAssets>(player._jsonAssets);
            playerAssets.Attributes.Infamy += malus;
            player._jsonAssets = JsonSerializer.Serialize<PlayerAssets>(playerAssets);
        }

        public static string ChooseRandomArmyName(IEnumerable<string> playerNames, IEnumerable<string> campaignNames)
        {
            var namesCount = References.ArmyNamesList.Length;
            var listNames = new List<string>(References.ArmyNamesList);
            if (namesCount > campaignNames.Count())
            {
                foreach(var usedName in campaignNames)
                {
                    listNames.RemoveAll(n => usedName.Contains(n));
                }
            } 
            else if (namesCount > playerNames.Count())
            {
                foreach (var usedName in playerNames)
                {
                    listNames.RemoveAll(n => usedName.Contains(n));
                }
            }

            var listFullNames = new List<string>();
            foreach(var unitName in References.UnitNamesList)
            {
                foreach(var armyName in listNames)
                {
                    listFullNames.Add(unitName + " " + armyName);
                }
            }
            if (listFullNames.Count() <= campaignNames.Count())
            {
                return Guid.NewGuid().ToString();
            }
            if (namesCount <= campaignNames.Count())
            {
                foreach (var usedName in campaignNames)
                {
                    listFullNames.RemoveAll(n => usedName.Contains(n));
                }
            }
            Random rnd = new Random();
            int rndIndex = rnd.Next(0, listFullNames.Count() - 1);
            return listFullNames[rndIndex];
        }
    }
}

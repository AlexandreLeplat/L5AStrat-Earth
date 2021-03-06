﻿using Entities.Database;
using Entities.Models;
using L5aStrat_Earth.Entities;
using Microsoft.EntityFrameworkCore;
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
            
            // S'il y a plus de suffixes dispos que de noms utilisés dans la campagne, on écarte les suffixes utilisés
            if (namesCount > campaignNames.Count())
            {
                foreach(var usedName in campaignNames)
                {
                    listNames.RemoveAll(n => usedName.EndsWith($" {n}"));
                }
            }
            // S'il y a plus de suffixes dispos que de noms utilisés par le joueur, on écarte les suffixes utilisés
            else if (namesCount > playerNames.Count())
            {
                foreach (var usedName in playerNames)
                {
                    listNames.RemoveAll(n => usedName.EndsWith($" {n}"));
                }
            }

            // On constitue la liste des combinaisons préfixes + suffixes possibles
            var listFullNames = new List<string>();
            foreach(var unitName in References.UnitNamesList)
            {
                foreach(var armyName in listNames)
                {
                    listFullNames.Add(unitName + " " + armyName);
                }
            }

            // On écarte les noms déjà utilisés
            if (namesCount <= campaignNames.Count())
            {
                foreach (var usedName in campaignNames)
                {
                    listFullNames.RemoveAll(n => usedName == n);
                }
            }

            // On sélectionne un nom au hasard dans la liste
            if (listFullNames.Any())
            {
                Random rnd = new Random();
                int rndIndex = rnd.Next(0, listFullNames.Count());
                return listFullNames[rndIndex];
            }
            // S'il ne reste aucun nom dispo, on rend un Guid aléatoire
            else return Guid.NewGuid().ToString();
        }

        public static Tuple<string,string> ChooseRandomColorAndPlayerName(IEnumerable<string> excludeColorList, IEnumerable<string> excludeNameList)
        {
            Random rnd = new Random();

            // Choix de la couleur et du nom de famille
            var clansList = new List<Clan>(References.ClansList);
            var color = string.Empty;
            var family = string.Empty;
            while (string.IsNullOrEmpty(color))
            {
                int rndIndex = rnd.Next(0, clansList.Count());
                var selectedClan = clansList[rndIndex];

                var colorsList = new List<string>(selectedClan.Colors);
                var familyNamesList = new List<string>(selectedClan.FamilyNames);
                colorsList.RemoveAll(c => excludeColorList.Contains(c));
                if (colorsList.Any() && familyNamesList.Any())
                {
                    rndIndex = rnd.Next(0, colorsList.Count());
                    color = colorsList[rndIndex];
                    rndIndex = rnd.Next(0, familyNamesList.Count());
                    family = familyNamesList[rndIndex];
                    if (family.StartsWith('[')) family = string.Empty;
                }
                else clansList.RemoveAt(rndIndex);
            }

            // Choix du prénom
            var firstNamesList = new List<string>();
            firstNamesList.AddRange(References.FirstNamesList);
            var firstName = string.Empty;
            while(string.IsNullOrEmpty(firstName))
            {
                if (firstNamesList.Any())
                {
                    int rndIndex = rnd.Next(0, firstNamesList.Count());
                    if (excludeNameList.Any(n => n.Contains(firstNamesList[rndIndex])))
                    {
                        firstNamesList.RemoveAt(rndIndex);
                    }
                    else
                    {
                        firstName = firstNamesList[rndIndex];
                    }
                }
                else firstName = Guid.NewGuid().ToString();
            }

            return new Tuple<string, string>(color, $"{family} {firstName}".Trim());
        }

        public static void SendNotification(long playerId, string subject, string body, DAL dal)
        {
            var adminId = (from p in dal.Players
                           join p2 in dal.Players on p.CampaignId equals p2.CampaignId
                           where p.IsAdmin && p2.Id == playerId
                           select p.Id).FirstOrDefault();

            var currentTurn = (from c in dal.Campaigns
                               join p in dal.Players on c.Id equals p.CampaignId
                               where p.Id == playerId
                               select c.CurrentTurn).FirstOrDefault();

            var message = new Message()
            {
                SenderId = adminId,
                PlayerId = playerId,
                Subject = subject,
                Body = body,
                IsNotification = true,
                SendDate = DateTime.Now,
                Turn = currentTurn
            };

            dal.Messages.Add(message);
            dal.SaveChanges();
        }
    }
}

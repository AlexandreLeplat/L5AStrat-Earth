using Entities.Database;
using Entities.Enums;
using Entities.Interfaces;
using Entities.Models;
using L5aStrat_Earth;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostApp.Business
{
    public class LobbyEngine
    {
        private readonly DAL _dal;

        private Dictionary<long, IGameEngine> _gameEngines;

        public LobbyEngine(DAL dal)
        {
            _dal = dal;
            _gameEngines = new Dictionary<long, IGameEngine>();
        }

        private void InitializeGameEngine(long idGame)
        {
            if (!_gameEngines.ContainsKey(idGame))
            {
                _gameEngines.Add(idGame, new L5aStratEarthEngine(_dal)); // TODO : rendre l'initialisation du moteur de jeu dynamique
            }
        }

        public bool LaunchGame(Campaign campaign)
        {
            this.InitializeGameEngine(campaign.GameId);
            if (campaign.Status != CampaignStatus.Preparation)
            {
                return false;
            }
            _gameEngines[campaign.GameId].InitCampaign(campaign);
            campaign.Status = CampaignStatus.Running;
            _gameEngines[campaign.GameId].BeginTurn(campaign);

            var launchDate = campaign.NextPhase;
            while (launchDate < DateTime.Now) launchDate = launchDate.AddDays(1);
            campaign.NextPhase = launchDate.AddMinutes(campaign.PhaseLength);
            campaign.CurrentTurn = 1;
            campaign.CurrentPhase = TurnPhase.Early;

            return true;
        }

        public Player GenerateRandomPlayer(Campaign campaign, long userId)
        {
            this.InitializeGameEngine(campaign.GameId);
            if (campaign.Status != CampaignStatus.Preparation)
            {
                return null;
            }
            var randomPlayer = _gameEngines[campaign.GameId].GenerateRandomPlayer(campaign.Id);
            randomPlayer.UserId = userId;

            return randomPlayer;
        }
    }
}

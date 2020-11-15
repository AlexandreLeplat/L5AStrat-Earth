using Entities.Database;
using Entities.Enums;
using Entities.Interfaces;
using Entities.Models;
using L5aStrat_Earth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HostApp.Business
{
    public class OrdersEngine
    {
        private readonly DAL _dal;

        private Dictionary<long, IGameEngine> _gameEngines;

        public OrdersEngine(DAL dal)
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

        public bool Work(Campaign campaign)
        {
            var isWorkDone = false;
            if (campaign.CurrentPhase == TurnPhase.Stop) return false;

            this.InitializeGameEngine(campaign.GameId);

            if (campaign.Status == CampaignStatus.Preparation)
            {
                _gameEngines[campaign.GameId].InitCampaign(campaign);
                campaign.Status = CampaignStatus.Running;
                isWorkDone = true;
            }

            while (campaign.NextPhase < DateTime.Now)
            {
                isWorkDone = true;
                switch (campaign.CurrentPhase)
                {
                    case TurnPhase.Early:
                        this.CheckEarlyPhaseOrders(campaign);
                        campaign.CurrentPhase = TurnPhase.Middle;
                        break;
                    case TurnPhase.Middle:
                        this.CheckMiddlePhaseOrders(campaign);
                        campaign.CurrentPhase = TurnPhase.Late;
                        break;
                    case TurnPhase.Late:
                        this.CheckLatePhaseOrders(campaign);
                        campaign.CurrentPhase = TurnPhase.End;
                        _gameEngines[campaign.GameId].EndTurn(campaign);
                        break;
                    case TurnPhase.End:
                        this.ChangeTurn(campaign);
                        break;
                }
                campaign.NextPhase = campaign.NextPhase.AddMinutes(campaign.PhaseLength);
            }

            if (campaign.CurrentPhase == TurnPhase.Middle)
            {
                isWorkDone = this.CheckMiddlePhaseOrders(campaign);
            }

            return isWorkDone;
        }

        private bool ChangeTurn(Campaign campaign)
        {
            if (campaign.CurrentTurn >= 12)
            {
                campaign.CurrentPhase = TurnPhase.Stop;
            } 
            else
            {
                campaign.CurrentTurn++;
                campaign.CurrentPhase = TurnPhase.Early;
                _gameEngines[campaign.GameId].BeginTurn(campaign);
            }

            return true;
        }

        private bool CheckEarlyPhaseOrders(Campaign campaign)
        {
            var ordersSheets = (from s in _dal.OrdersSheets
                                join p in _dal.Players on s.PlayerId equals p.Id
                                where p.CampaignId == campaign.Id && s.Status == OrdersSheetStatus.Planned
                                    && s.SendDate < campaign.NextPhase
                                orderby s.Priority descending, s.SendDate ascending
                                select s).ToList();

            foreach (var sheet in ordersSheets)
            {
                sheet.Status = OrdersSheetStatus.Treating;
                _dal.SaveChanges();

                sheet.Player = _dal.Players.FirstOrDefault(p => p.Id == sheet.PlayerId);
                sheet.Priority = _gameEngines[campaign.GameId].PayPriority(sheet.Player, sheet.Priority);
            }
            foreach (var sheet in ordersSheets.OrderByDescending(s => s.Priority))
            {
                sheet.Orders = _dal.Orders.Where(o => o.OrdersSheetId == sheet.Id).OrderBy(o => o.Rank).ToList();
                foreach (var order in sheet.Orders)
                {
                    _gameEngines[campaign.GameId].ProcessOrder(order);
                }
                _gameEngines[campaign.GameId].EndSheet(sheet.Player);
                sheet.Status = OrdersSheetStatus.Completed;
                _dal.SaveChanges();
            }
            return true;
        }

        private bool CheckMiddlePhaseOrders(Campaign campaign)
        {
            var ordersSheets = (from s in _dal.OrdersSheets
                                join p in _dal.Players on s.PlayerId equals p.Id
                                where p.CampaignId == campaign.Id && s.Status == OrdersSheetStatus.Planned
                                    && s.SendDate < campaign.NextPhase
                                orderby s.SendDate ascending
                                select s).ToList();

            foreach (var sheet in ordersSheets)
            {
                if (sheet.Status != OrdersSheetStatus.Planned) continue;
                sheet.Status = OrdersSheetStatus.Treating;
                _dal.SaveChanges();

                sheet.Player = _dal.Players.FirstOrDefault(p => p.Id == sheet.PlayerId);
                sheet.Orders = _dal.Orders.Where(o => o.OrdersSheetId == sheet.Id).OrderBy(o => o.Rank).ToList();

                foreach (var order in sheet.Orders)
                {
                    _gameEngines[campaign.GameId].ProcessOrder(order);
                }
                _gameEngines[campaign.GameId].EndSheet(sheet.Player);
                sheet.Status = OrdersSheetStatus.Completed;
                _dal.SaveChanges();
            }
            return true;
        }

        private bool CheckLatePhaseOrders(Campaign campaign)
        {
            var ordersSheets = (from s in _dal.OrdersSheets
                                join p in _dal.Players on s.PlayerId equals p.Id
                                where p.CampaignId == campaign.Id && s.Status == OrdersSheetStatus.Planned
                                    && s.SendDate < campaign.NextPhase
                                orderby s.Priority ascending, s.SendDate ascending
                                select s).ToList();

            foreach (var sheet in ordersSheets)
            {
                sheet.Status = OrdersSheetStatus.Treating;
                _dal.SaveChanges();

                sheet.Player = _dal.Players.FirstOrDefault(p => p.Id == sheet.PlayerId);
                sheet.Priority = _gameEngines[campaign.GameId].PayPriority(sheet.Player, sheet.Priority);
            }
            foreach (var sheet in ordersSheets.OrderBy(s => s.Priority))
            {
                sheet.Orders = _dal.Orders.Where(o => o.OrdersSheetId == sheet.Id).OrderBy(o => o.Rank).ToList();
                foreach (var order in sheet.Orders)
                {
                    _gameEngines[campaign.GameId].ProcessOrder(order);
                }
                _gameEngines[campaign.GameId].EndSheet(sheet.Player);
                sheet.Status = OrdersSheetStatus.Completed;
                _dal.SaveChanges();
            }
            return true;
        }
    }
}

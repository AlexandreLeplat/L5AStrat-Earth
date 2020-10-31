using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Interfaces
{
    public interface IGameEngine
    {
        public void InitCampaign(Campaign campaign);
            
        public bool PayPriority(Player player, int priority);

        public List<Order> CheckOrdersSheet(OrdersSheet sheet);

        public Order ProcessOrder(Order order);

        public Dictionary<string, string> GetOptionsList(string resource, long playerId, IEnumerable<string> parameters);

        public bool EndSheet(Player player);

        public bool BeginTurn(Campaign campaign);

        public bool EndTurn(Campaign campaign);
    }
}

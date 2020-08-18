using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class OrdersSheet
    {
        public long Id { get; set; }
        
        // Numéro du tour
        public int Turn { get; set; }

        // Nombre de points de priorité joués
        public int Priority { get; set; }

        // Nombre maximum d'actions jouables
        public int MaxOrdersCount { get; set; }

        // Date d'envoi de la feuille
        public DateTime? SendDate { get; set; }

        // Joueur auquel la feuille d'ordre appartient
        public long PlayerId { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }

        // Ordres contenus dans la feuille
        [JsonIgnore]
        public List<Order> Orders { get; set; }
    }
}

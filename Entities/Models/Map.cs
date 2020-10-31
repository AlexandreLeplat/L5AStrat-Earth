using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Map
    {
        public long Id { get; set; }

        // Nom de la carte
        public string Name { get; set; }

        // Numéro du tour
        public int Turn { get; set; }

        // Date d'édition de la carte
        public DateTime CreationDate { get; set; }

        [JsonIgnore]
        public List<MapTile> MapTiles { get; set; }

        // La campagne à laquelle la carte appartient
        public long CampaignId { get; set; }
        [JsonIgnore]
        public Campaign Campaign { get; set; }
        
        // Joueur auquel la carte appartient
        public long PlayerId { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }
    }
}

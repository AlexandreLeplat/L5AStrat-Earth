using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Campaign
    {
        public long Id { get; set; }

        public string Name { get; set; }

        // Date de la prochaine phase
        public DateTime NextPhase { get; set; }

        // Durée d'une phase en minutes
        public int PhaseLength { get; set; }

        // Numéro de la phase en cours
        public TurnPhase CurrentPhase { get; set; }
        // Numéro du tour en cours
        public int CurrentTurn { get; set; }

        // Statut de la campagne
        public CampaignStatus Status { get; set; }

        // Liste des caractéristiques de la campagne regroupées par catégories
        [NotMapped]
        public Dictionary<string, Dictionary<string, string>> Assets
        {
            get { return _jsonAssets == null ? null : JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(_jsonAssets); }
            set { _jsonAssets = JsonSerializer.Serialize(value); }
        }
        [JsonIgnore]
        public string _jsonAssets { get; set; }

        public long GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }

        [JsonIgnore]
        public List<Player> Players { get; set; }

        // Liste des cartes de la campagne
        [JsonIgnore]
        public List<Map> Maps { get; set; }
    }
}

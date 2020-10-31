using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Player
    {
        public long Id { get; set; }

        public string Name { get; set; }

        // La couleur du joueur sur la carte
        public string Color { get; set; }
        
        // Liste des caractéristiques du joueur regroupées par catégories
        [NotMapped]
        public Dictionary<string, Dictionary<string, string>> Assets 
        {
            get { return _jsonAssets == null ? null : JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(_jsonAssets); }
            set { _jsonAssets = JsonSerializer.Serialize(value); }
        }
        [JsonIgnore]
        public string _jsonAssets { get; set; }

        // Flag indiquant si une nouvelle carte est disponible
        public bool HasNewMap { get; set; }

        // Flag indiquant s'il s'agit du joueur actuellement utilisé par l'utilisateur (dans le cadre d'un utilisateur multi-comptes)
        [JsonIgnore]
        public bool IsCurrentPlayer { get; set; }

        // Flag indiquant s'il s'agit d'un compte d'administration
        [JsonIgnore]
        public bool IsAdmin { get; set; }

        // L'utilisateur qui possède ce compte joueur
        public long UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        // La campagne sur laquelle le joueur est inscrit
        public long CampaignId { get; set; }
        [JsonIgnore]
        public Campaign Campaign { get; set; }

        // Liste des cartes du joueur
        [JsonIgnore]
        public List<Map> Maps { get; set; }

        // Liste des unités du joueur
        [JsonIgnore]
        public List<Unit> Units { get; set; }

        // Liste des messages du joueur
        [JsonIgnore] 
        public List<Message> Messages { get; internal set; }
    }
}

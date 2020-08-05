using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Game
    {
        public long Id { get; set; }

        public string Name { get; set; }

        // Liste des widgets de la page d'accueil
        [NotMapped]
        public List<string> HomeWidgets
        {
            get { return _jsonHomeWidgets == null ? null : JsonSerializer.Deserialize<List<string>>(_jsonHomeWidgets); }
            set { _jsonHomeWidgets = JsonSerializer.Serialize(value); }
        }
        [JsonIgnore]
        public string _jsonHomeWidgets { get; set; }

        [JsonIgnore]
        public List<Campaign> Campaigns { get; set; }
    }
}

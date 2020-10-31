using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Unit
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public long PlayerId { get; set; }
        public Player Player { get; set; }

        // Attributs de l'unité, classés par catégorie
        [NotMapped]
        public Dictionary<string, Dictionary<string, string>> Assets
        {
            get { return _jsonAssets == null ? null : JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(_jsonAssets); }
            set { _jsonAssets = JsonSerializer.Serialize(value); }
        }

        [JsonIgnore]
        public string _jsonAssets { get; set; }
    }
}

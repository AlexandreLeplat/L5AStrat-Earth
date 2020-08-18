using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class ActionType
    {
        public long Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        
        [NotMapped]
        public List<OrderInput> Form
        {
            get { return _jsonForm == null ? null : JsonSerializer.Deserialize<List<OrderInput>>(_jsonForm); }
            set { _jsonForm = JsonSerializer.Serialize(value); }
        }
        [JsonIgnore]
        public string _jsonForm { get; set; }

        // Le système de jeu dans lequel ces actions sont disponibles
        public long GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; }
    }
}
